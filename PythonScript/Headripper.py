
#!/usr/bin/env python3
from __future__ import annotations

import argparse, base64 as _b64, json, os, platform, shutil, subprocess, sys, time as _time
from pathlib import Path
from typing import Optional, Tuple
import subprocess

from headspace_endpoints import map_categories_vm, map_focus_topics_module  # make sure both are imported


# -------- constants --------
API_BASE = "https://api.prod.headspace.com"
SAVED_DIR = Path("Saved")
ENV_PATH = SAVED_DIR / "BearerID.env"
FFMPEG_REPO_URL = "https://github.com/Joexv/HeadRipper/raw/refs/heads/master/ffmpeg.exe"

# -------- external modules --------
from api_client import HeadspaceAPI
from headspace_endpoints import HeadspaceService
try:
    from headspace_headers import build_headers  # correct name in your file
except ImportError:
    build_headers = None  # probe will guard against None

# -------- logging --------
def info(msg: str): print(f"[info] {msg}")
def ok(msg: str): print(f"[ok] {msg}")
def warn(msg: str): print(f"[warn] {msg}")
def err(msg: str): print(f"[err] {msg}")

# -------- fs --------
def ensure_saved_dir():
    SAVED_DIR.mkdir(parents=True, exist_ok=True)
    (SAVED_DIR / "cache" / "api").mkdir(parents=True, exist_ok=True)

# -------- token helpers --------
def read_hsngjwt_from_envfile(path: Path) -> str | None:
    try:
        text = path.read_text(encoding="utf-8")
    except FileNotFoundError:
        return None
    for line in text.splitlines():
        s = line.strip()
        if not s: continue
        if s.startswith("HSNGJWT="):
            return s.split("=", 1)[1].strip()
    text = text.strip()
    if "." in text and len(text) > 100:
        return text
    return None

def _b64url_to_json(seg: str):
    try:
        pad = "=" * (-len(seg) % 4)
        return json.loads(_b64.urlsafe_b64decode((seg + pad).encode()).decode("utf-8", "ignore"))
    except Exception:
        return None

def jwt_parse(token: str):
    parts = token.split(".")
    if len(parts) < 2: return None, None
    return _b64url_to_json(parts[0]), _b64url_to_json(parts[1])

def jwt_is_valid_now(token: str, skew: int = 120):
    _, payload = jwt_parse(token)
    if not payload: return False, None, "bad-token"
    exp = payload.get("exp")
    if exp is None: return True, payload, "no-exp"
    now = int(_time.time())
    return (now + skew < int(exp)), payload, ("ok" if now + skew < int(exp) else "expired")

def jwt_user_id_from_payload(payload: dict) -> Optional[str]:
    # hsId claim looks like: "https://api.prod.headspace.com/hsId": "HSUSER_...."
    return (payload or {}).get("https://api.prod.headspace.com/hsId")


# -------- subprocess --------
def run(cmd: list[str], check=False) -> Tuple[int, str, str]:
    try:
        p = subprocess.run(cmd, capture_output=True, text=True, check=check)
        return p.returncode, p.stdout.strip(), p.stderr.strip()
    except Exception as e:
        return 1, "", str(e)

# -------- login --------
def run_browser_login(use_creds: bool, save_creds: bool, headless: bool) -> bool:
    script = Path(__file__).parent / "Browser_Login.py"
    if not script.exists():
        err(f"Missing {script.name}."); return False
    cmd = [sys.executable, str(script)]
    if use_creds: cmd.append("--use-creds")
    if save_creds: cmd.append("--save-creds")
    if headless: cmd.append("--headless")
    info("Launching browser login helper...")
    code, out, serr = run(cmd)
    if code != 0:
        err(f"Login helper failed:\n{serr}\n{out}"); return False
    ok("Browser login flow completed."); return True

def prompt_yn(msg: str, default: bool = False) -> bool:
    suffix = " [Y/n]: " if default else " [y/N]: "
    try: ans = input(msg + suffix).strip().lower()
    except EOFError: return default
    if ans == "": return default
    return ans in ("y", "yes")

def ensure_token(*, relogin=False, headless_browser=False) -> str:
    token = read_hsngjwt_from_envfile(ENV_PATH)
    if relogin or not token:
        # save_creds = prompt_yn("Save your credentials to OS keyring for autofill?", default=False)
        # use_creds  = save_creds or prompt_yn("Use saved credentials for this login (if available)?", default=True)
        save_creds = False
        use_creds  = False #Credential saving not working rn. Mostly psuedo code.
        if not run_browser_login(use_creds=use_creds, save_creds=save_creds, headless=headless_browser):
            raise SystemExit(4)
        token = read_hsngjwt_from_envfile(ENV_PATH)
        if not token: raise SystemExit("Login done but no token in Saved/BearerID.env")
    valid, _, reason = jwt_is_valid_now(token)
    if not valid:
        warn(f"JWT {reason}. Re-running login...")
        if not run_browser_login(use_creds=True, save_creds=False, headless=headless_browser):
            raise SystemExit(5)
        token = read_hsngjwt_from_envfile(ENV_PATH) or ""
        valid, _, reason = jwt_is_valid_now(token)
        if not valid: raise SystemExit("Relogin produced invalid/expired token.")
        ok("Refreshed token captured from Saved/BearerID.env")
    return token

# -------- ffmpeg --------
def which_ffmpeg() -> Optional[str]:
    return shutil.which("ffmpeg")

def local_ffmpeg_candidate() -> Optional[str]:
    cand = Path(__file__).parent / ("ffmpeg.exe" if platform.system()=="Windows" else "ffmpeg")
    if cand.exists():
        os.environ["PATH"] = str(cand.parent) + os.pathsep + os.environ.get("PATH", "")
        return str(cand)
    return None

def download_ffmpeg_to_root() -> Optional[str]:
    if platform.system() != "Windows": return None
    dest = Path(__file__).parent / "ffmpeg.exe"
    try:
        import urllib.request
        info(f"Downloading fallback ffmpeg: {FFMPEG_REPO_URL}")
        with urllib.request.urlopen(FFMPEG_REPO_URL) as r, open(dest,"wb") as f:
            while True:
                chunk = r.read(8192)
                if not chunk:
                    break
                f.write(chunk)
        if not dest.exists() or dest.stat().st_size < 100*1024:
            warn("ffmpeg.exe seems too small; ignoring."); return None
        os.environ["PATH"] = str(dest.parent)+os.pathsep+os.environ.get("PATH","")
        code, out, errm = run([str(dest),"-version"])
        if code == 0: ok(f"Using local ffmpeg at {dest}"); return str(dest)
        warn(f"ffmpeg.exe failed to run: {errm or out}")
    except Exception as e: warn(f"ffmpeg download failed: {e}")
    return None

def ensure_ffmpeg() -> Optional[str]:
    exe = which_ffmpeg()
    if exe: ok(f"ffmpeg found: {exe}"); return exe
    local = local_ffmpeg_candidate()
    if local: ok(f"ffmpeg found in script root: {local}"); return local
    warn("ffmpeg not found. Attempting install...")
    if platform.system()=="Windows":
        warn("Package manager install failed, using repo fallback (not feature complete).")
        return download_ffmpeg_to_root()
    err("Automatic ffmpeg provisioning failed."); return None

# -------- seeds & categories --------
SEED_TOPICS = {
    "SLEEP": [
        {"id": 41, "name": "Sleepcasts"},
        {"id": 89, "name": "Kids and parents"},
        {"id": 48, "name": "Sleep radio"},
        {"id": 43, "name": "Wind downs"},
        {"id": 42, "name": "Sleep music"},
        {"id": 86, "name": "Eve's guide to sleep"},
        {"id": 44, "name": "Soundscapes"},
    ],
    "MEDITATE": [
        {"id": 60, "name": "Timers"},
        {"id": 61, "name": "Techniques and support"},
        {"id": 58, "name": "Courses and singles"},
    ],
    "FOCUS": [
        {"id": 211,"name": "Binaural Beats Focus"},
        {"id": 176,"name": "White Noise and Sleep Sounds Explore"},
        {"id": 62,"name": "focus Music"},
        {"id": 65,"name": "Soundscapes"},
        {"id": 175,"name": "Sleep Music Focus"},
        {"id": 177,"name": "Sleep Radio Focus"}
    ],
}

def _read_json_if_exists(path: Path):
    try:
        return json.loads(path.read_text(encoding="utf-8"))
    except Exception:
        return None

def _probe_token(api_base: str, token: str, headers: dict, endpoint: str, accept_3xx: bool) -> tuple[bool, int, str]:
    import requests
    url = api_base.rstrip("/") + "/" + endpoint.lstrip("/")
    try:
        r = requests.get(url, headers=headers, timeout=12, allow_redirects=False)
        code = r.status_code
        okk = (200 <= code < 300) or (accept_3xx and 300 <= code < 400)
        return okk, code, r.text[:200] if r.text else ""
    except Exception as e:
        return False, -1, str(e)

def _vm_roots(vm: dict) -> list[dict]:
    for key in ("items", "tiles", "cards", "results"):
        v = vm.get(key)
        if isinstance(v, list):
            return v
    data = vm.get("data")
    if isinstance(data, dict):
        for key in ("items", "tiles", "cards", "results"):
            v = data.get(key)
            if isinstance(v, list):
                return v
    return []

def map_categories_vm(vm: dict) -> list[dict]:
    cats = []
    roots = _vm_roots(vm)
    for t in roots:
        if not isinstance(t, dict): continue
        tid = t.get("topicId") or t.get("id") or t.get("topic_id")
        name = t.get("title") or t.get("name")
        order = t.get("ordinal") or t.get("order") or t.get("sortOrder") or t.get("displayOrder")
        if tid is None: continue
        cats.append({"id": tid, "name": name, "order": order})
    cats.sort(key=lambda x: (x.get("order") is None, x.get("order"), str(x.get("name") or "")))
    return cats

def flatten_viewmodel_items(vm: dict) -> list[dict]:
    items = []
    roots = _vm_roots(vm)
    for t in roots:
        if not isinstance(t, dict): continue
        cid = t.get("contentId") or t.get("id")
        ctype = (t.get("contentType") or t.get("type") or "").upper()
        title = t.get("title") or t.get("name")
        desc = t.get("description") or t.get("summary")
        entity_id = t.get("entityId") or t.get("entity_id")
        items.append({
            "contentId": str(cid) if cid is not None else None,
            "contentType": ctype,
            "entityId": str(entity_id) if entity_id is not None else None,
            "title": title,
            "description": desc,
        })
    return items

def extract_variants_from_viewmodel(vm: dict, *, container: str = "aac") -> dict[str, list[dict]]:
    from collections import defaultdict
    by_cid = defaultdict(list)
    tiles = _vm_roots(vm)

    def dig(obj, parent_cid: str|int):
        if isinstance(obj, dict):
            cid = obj.get("contentId") or obj.get("id")
            if cid is not None and str(cid) != str(parent_cid):
                dur = obj.get("duration") or obj.get("length") or obj.get("audioDuration")
                narrator = obj.get("narrator") or obj.get("narratorName") or obj.get("voice")
                name = obj.get("title") or obj.get("name") or obj.get("displayName")
                if (str(cid).isdigit() or dur or narrator or name):
                    by_cid[str(parent_cid)].append({
                        "contentId": str(cid),
                        "title": name,
                        "narrator": narrator,
                        "duration": dur,
                        "container": container,
                        "downloadUrl": f"{API_BASE}/content-aggregation/v1/content/media-items/{cid}/download?container={container}"
                    })
            for v in obj.values():
                dig(v, parent_cid)
        elif isinstance(obj, list):
            for v in obj:
                dig(v, parent_cid)

    for t in tiles:
        if not isinstance(t, dict): continue
        parent = t.get("contentId") or t.get("id")
        if parent is None: continue
        dig(t, parent)
        seen = set(); uniq = []
        for v in by_cid[str(parent)]:
            k = v["contentId"]
            if k in seen: continue
            seen.add(k); uniq.append(v)
        by_cid[str(parent)] = uniq
    return by_cid

def resolve_categories(svc, location: str, *, ttl: int = 6*3600, user_file: Path | None = None) -> list[dict]:
    """
    Live fetch -> cache -> user overrides -> seeds. De-dupes by id and saves a consolidated file.
    """
    loc = location.upper()

    # Focus uses a different endpoint; handled elsewhere – fall through for others
    if loc == "FOCUS":
        try:
            _, payload = jwt_parse(svc.token)
            user_id = (payload or {}).get("https://api.prod.headspace.com/hsId")
            if not user_id:
                raise RuntimeError("Missing hsId in JWT payload")
            vm = svc.focus_topics_module(user_id, ttl=ttl)
            cats = map_focus_topics_module(vm)
            (SAVED_DIR / f"categories_{location}.json").write_text(
                json.dumps(cats, indent=2, ensure_ascii=False), encoding="utf-8"
            )
            ok(f"{len(cats)} categories for {location} (focus v2)")
            return cats
        except Exception as e:
            warn(f"focus topics-module failed ({e}); falling back to cache/overrides/seeds")

    # Try live v1 topics-menu first
    try:
        vm = svc.topics_menu(location, ttl=ttl)
        cats = map_categories_vm(vm)
        (SAVED_DIR / f"categories_{location}.json").write_text(
            json.dumps(cats, indent=2, ensure_ascii=False), encoding="utf-8"
        )
        ok(f"{len(cats)} categories for {location} (live)")
        return cats
    except RuntimeError as e:
        if "404" not in str(e) and "Not Found" not in str(e):
            # Non-404 errors re-raised
            raise
        warn("topics-menu 404; falling back to cache/overrides/seeds")

    pool: list[dict] = []

    cached = _read_json_if_exists(SAVED_DIR / f"categories_{location}.json")
    if isinstance(cached, list) and cached:
        ok(f"Using cached categories for {location}")
        pool.extend(cached)

    if user_file and user_file.exists():
        try:
            user = json.loads(user_file.read_text(encoding="utf-8"))
            if isinstance(user, dict):
                user = user.get(location) or []
            if isinstance(user, list):
                ok(f"Using user override categories from {user_file}")
                pool.extend(user)
        except Exception:
            warn(f"Failed to parse categories override file: {user_file}")

    seeds = SEED_TOPICS.get(loc, [])
    if seeds:
        pool.extend(seeds)

    dedup = {}
    for c in pool:
        cid = str(c.get("id"))
        if cid and cid not in dedup:
            dedup[cid] = {
                "id": int(cid) if cid.isdigit() else cid,
                "name": c.get("name"),
                "order": c.get("order"),
            }

    cats = list(dedup.values())
    if not cats:
        raise SystemExit(f"No categories available for {location}. Provide --topic-id or a categories override file.")

    (SAVED_DIR / f"categories_{location}.json").write_text(
        json.dumps(cats, indent=2, ensure_ascii=False), encoding="utf-8"
    )
    ok(f"{len(cats)} categories for {location} (fallback)")
    return cats


def _get_json_retry(api, path, token, *, auth_mode, headers, ttl, retries=3, backoff=0.75):
    import time
    last_exc = None
    for i in range(retries):
        try:
            return api.get_json(path, token, auth_mode=auth_mode, ttl=ttl, headers=headers)
        except Exception as e:
            last_exc = e
            if i < retries - 1:
                time.sleep(backoff * (2 ** i))
    raise last_exc

def build_catalog(
    token: str,
    *,
    location: str,
    topic_id: str | None,
    client: str,
    client_version: str,
    auth_mode: str,
    max_items: int | None,
    make_index: bool,
    container: str,
    categories_file: Path | None,
) -> None:
    api = HeadspaceAPI(api_base=API_BASE, cache_dir=SAVED_DIR / "cache" / "api")
    svc = HeadspaceService(api, token, auth_mode=auth_mode, client_profile=client, client_version=client_version)

    if topic_id is None:
        cats = resolve_categories(svc, location, ttl=6*3600, user_file=categories_file)
        topics = cats
    else:
        topics = [{"id": str(topic_id), "name": f"Topic {topic_id}", "order": None}]

    for cat in topics:
        tid = str(cat["id"])
        title = cat.get("name") or tid
        info(f"== Topic {tid}: {title} ==")

        path = ("/content-aggregation/v1/content/view-models/library/topics-category-menu"
                f"?location={location}&tag=&topicId={tid}")
        vm = _get_json_retry(api, path, token, auth_mode=auth_mode, headers=svc.headers, ttl=3600)

        raw_path = SAVED_DIR / f"viewmodel_{location}_{tid}.json"
        raw_path.write_text(json.dumps(vm, indent=2, ensure_ascii=False), encoding="utf-8")

        # items
        items = flatten_viewmodel_items(vm)
        if max_items and len(items) > max_items:
            items = items[:max_items]
        # No need to save the items since we are reading the JSONs directly. Parsing them out has no real world benefit at this time.
        # (SAVED_DIR / f"items_{location}_{tid}.json").write_text(json.dumps(items, indent=2, ensure_ascii=False), encoding="utf-8")
        # ok(f"{len(items)} items saved for topic {tid}")

        # variants
        variants_by_cid = extract_variants_from_viewmodel(vm, container=container)
        out_variants = SAVED_DIR / f"variants_{location}_{tid}.json"
        # out_variants.write_text(json.dumps(variants_by_cid, indent=2, ensure_ascii=False), encoding="utf-8")

        with_variants = sum(1 for k, v in variants_by_cid.items() if v)
        empty = len(items) - with_variants
        print(f"[summary] topic {tid}: items={len(items)} with_variants={with_variants} empty={empty} -> {out_variants}")

        if make_index:
            index = {
                "location": location,
                "topicId": int(tid) if tid.isdigit() else tid,
                "category": {"id": cat["id"], "name": title},
                "items": items,
                "variantsByContentId": variants_by_cid,
            }
            out_index = SAVED_DIR / f"index_{location}_{tid}.json"
            # out_index.write_text(json.dumps(index, indent=2, ensure_ascii=False), encoding="utf-8")
            # ok(f"Wrote merged index to {out_index}")

# -------- main --------
def main() -> int:
    ap = argparse.ArgumentParser(description="Headspace Local Client — unified builder")
    ap.add_argument("--location", default="SLEEP", help="Top-level section: SLEEP, MEDITATE, FOCUS, etc.")
    ap.add_argument("--topic-id", help="Process a single topic id; omit to show all topics or use --all-topics")
    ap.add_argument("--all-topics", action="store_true", help="Process all topics under the location")
    ap.add_argument("--max-items", type=int, default=None, help="Limit items per topic (testing)")
    ap.add_argument("--container", choices=["aac", "mp3"], default="aac", help="Download container format")

    ap.add_argument("--probe", dest="do_probe", action="store_true", help="Enable a token/http probe before API calls")
    ap.add_argument("--no-probe", dest="do_probe", action="store_false", help="Disable the probe")
    ap.set_defaults(do_probe=False)
    ap.add_argument("--probe-endpoint", default="", help="Optional absolute path to probe (e.g. /content-aggregation/v1/...)")
    ap.add_argument("--probe-accept-3xx", action="store_true", help="Treat 3xx as success during the probe")

    ap.add_argument("--auth-mode", choices=["auto", "bearer", "cookie"], default="bearer",
                    help="Auth mode (bearer always sends Authorization, cookie adds hsngjwt)")
    ap.add_argument("--client", choices=["ios", "web"], default="ios", help="Client profile; ios shows app-only content")
    ap.add_argument("--client-version", default="301190000", help="Client version string")

    ap.add_argument("--relogin", action="store_true", help="Force fresh login to refresh HSNGJWT")
    ap.add_argument("--headless-browser", action="store_true", help="Headless playwright login (not recommended)")
    ap.add_argument("--no-index", action="store_true", help="Skip merged index file")
    ap.add_argument("--categories-file", default=str(SAVED_DIR / "categories_overrides.json"),
                    help="Optional JSON file for category overrides (array for current location or map of location->array)")
    ap.add_argument("--debug-http", action="store_true", help="Verbose HTTP debug (sets HR_DEBUG_HTTP=1)")

    args = ap.parse_args()

    if args.debug_http:
        os.environ["HR_DEBUG_HTTP"] = "1"
        print("[dbg] HTTP debug enabled")

    ensure_saved_dir()
    if not ensure_ffmpeg():
        return 2

    token = ensure_token(relogin=args.relogin, headless_browser=args.headless_browser)

    if args.do_probe:
        if build_headers is None:
            warn("Probe requested but headspace_headers.build_headers is unavailable; skipping probe.")
        else:
            if args.probe_endpoint:
                probe_ep = args.probe_endpoint
            else:
                if args.all_topics:
                    probe_ep = f"/content-aggregation/v1/content/view-models/library/topics-menu?location={args.location}"
                else:
                    tid = args.topic_id or "41"
                    probe_ep = f"/content-aggregation/v1/content/view-models/library/topics-category-menu?location={args.location}&topicId={tid}"
            probe_headers = build_headers(profile=args.client, version=args.client_version)
            probe_headers["Authorization"] = f"Bearer {token}"
            ok_probe, status, _ = _probe_token(API_BASE, token, probe_headers, probe_ep, accept_3xx=args.probe_accept_3xx)
            if ok_probe: ok(f"Probe OK ({status}) on {probe_ep}")
            else: warn(f"Probe failed ({status}) on {probe_ep}. Continuing anyway.")

    chosen = args.auth_mode if args.auth_mode != "auto" else "bearer"
    ok(f"Auth mode selected: {chosen}")

    topic_id = None if args.all_topics else args.topic_id

    build_catalog(
        token,
        location=args.location,
        topic_id=topic_id,
        client=args.client,
        client_version=args.client_version,
        auth_mode=chosen,
        max_items=args.max_items,
        make_index=(not args.no_index),
        container=args.container,
        categories_file=Path(args.categories_file) if args.categories_file else None,
    )

    ok("Done with loading the catalog.")
    
    subprocess.run(["python", "Download_Audio.py"])
    return 0

if __name__=="__main__":
    try: raise SystemExit(main())
    except KeyboardInterrupt: print("\n[abort] interrupted by user"); raise SystemExit(130)
