# headspace_variants.py
from __future__ import annotations
from typing import Any, Dict, Iterable, List
import hashlib, re
import requests
from datetime import date

AUDIO_EXT = (".mp3", ".m4a", ".aac", ".wav", ".flac", ".ogg")
HLS_HINTS = ("m3u8", "application/vnd.apple.mpegurl")

def _iter_nodes(obj: Any) -> Iterable[tuple[list[str], Any]]:
    def rec(node, path):
        if isinstance(node, dict):
            yield path, node
            for k, v in node.items():
                yield from rec(v, path + [str(k)])
        elif isinstance(node, list):
            yield path, node
            for i, v in enumerate(node):
                yield from rec(v, path + [str(i)])
        else:
            yield path, node
    yield from rec(obj, [])

def _coerce_int(x) -> int | None:
    try:
        if x is None: return None
        if isinstance(x, (int, float)): return int(x)
        s = str(x).strip()
        if s.endswith("ms"): return int(int(s[:-2]) / 1000)
        return int(float(s))
    except Exception:
        return None

def _norm_lang(s: str | None) -> str | None:
    if not s: return None
    s = str(s).strip().replace("_", "-")
    if len(s) == 2 or (len(s) == 5 and "-" in s): return s
    m = re.search(r"[a-z]{2}(?:-[A-Z]{2})?", s)
    return m.group(0) if m else s

def _infer_filetype(url: str) -> str:
    u = url.lower()
    if any(ext in u for ext in AUDIO_EXT): return u.rsplit(".", 1)[-1]
    if "m3u8" in u: return "hls"
    return "unknown"

def _infer_quality(node: dict) -> tuple[str | None, int | None]:
    for k in ("bitrate", "bit_rate", "kbps", "bitrateKbps"):
        if k in node:
            try:
                val = int(node[k])
                return f"{val}kbps", val
            except Exception:
                pass
    for k in ("quality", "audioQuality", "variant", "name"):
        v = node.get(k)
        if isinstance(v, str) and v:
            m = re.search(r"(\d+)\s*kbps", v, re.I)
            if m:
                kb = int(m.group(1))
                return f"{kb}kbps", kb
            if v.lower() in ("low","medium","high","very_high","ultra","hq","lq","mq"):
                return v, None
    return None, None

def _probable_audio_node(node: dict) -> bool:
    url = node.get("url") or node.get("src") or node.get("href")
    if not isinstance(url, str): return False
    u = url.lower()
    return any(ext in u for ext in AUDIO_EXT) or any(h in u for h in HLS_HINTS)

def extract_variants(detail_json: Dict[str, Any], content_id: str | int) -> List[Dict[str, Any]]:
    variants: list[dict] = []
    for path, node in _iter_nodes(detail_json):
        if not isinstance(node, dict): continue
        if not _probable_audio_node(node): continue

        url = node.get("url") or node.get("src") or node.get("href")
        narrator = node.get("narrator") or node.get("voice") or node.get("voicedBy") or node.get("narration")
        language = node.get("language") or node.get("locale") or node.get("trackLanguage")
        if isinstance(language, dict):
            language = language.get("code") or language.get("locale") or language.get("language")
        language = _norm_lang(language)

        parent_meta = {}
        if len(path) >= 1:
            cur = detail_json
            for p in path[:-1]:
                try:
                    cur = cur[int(p)] if p.isdigit() else cur[p]
                except Exception:
                    cur = None
                    break
            if isinstance(cur, dict):
                parent_meta = cur

        length = _coerce_int(
            node.get("duration") or node.get("durationInSeconds") or
            node.get("length") or node.get("lengthSeconds") or
            parent_meta.get("duration") or parent_meta.get("durationInSeconds")
        )
        qstr, kbps = _infer_quality({**parent_meta, **node})
        file_type = _infer_filetype(url)
        hsrc = f"{url}|{narrator or ''}|{language or ''}".encode("utf-8", "ignore")
        variant_id = hashlib.sha1(hsrc).hexdigest()[:16]

        variants.append({
            "contentId": content_id,
            "variantId": variant_id,
            "mediaId": None,          # unknown in generic flow
            "role": None,             # unknown in generic flow
            "narrator": narrator,
            "language": language,
            "lengthSec": length,
            "quality": qstr,
            "bitrateKbps": kbps,
            "fileType": file_type,
            "url": url,
        })
    seen = set()
    uniq = []
    for v in variants:
        key = (v["url"], v.get("narrator"), v.get("language"))
        if key in seen: continue
        seen.add(key)
        uniq.append(v)
    return uniq


# headspace_variants.py

def fetch_sleepcast_variants_v3(content_id: str | int, headers: dict, *,
                                date_override: str | None = None,
                                include_split: bool = False) -> list[dict]:
    """
    Fetch Sleepcast variants (VOICE, AMBIENCE, MIXED) from the v3 playable-assets endpoint.
    - date_override: string 'YYYY-MM-DD'. Defaults to today.
    - include_split: if True, return VOICE + AMBIENCE + MIXED. If False, return MIXED only.
    """
    from datetime import date
    target_date = date_override or date.today().isoformat()

    url = (f"https://api.prod.headspace.com/content-interface/v3/playable-assets"
           f"?contentId={content_id}&date={target_date}&parentContentId=&audioDescriptionEnabled=false")
    print(url)
    input("Press Enter to continue...")
    resp = requests.get(url, headers=headers)
    if not resp.ok:
        raise RuntimeError(f"[err] v3 playable-assets failed for {content_id}: {resp.status_code}")
    data = resp.json()
    print(data)
    input("Press Enter to continue...")
    variants = []
    for entry in data:
        mid = entry.get("mediaItemId")
        vid = entry.get("id")            # e.g. "SC-408-VOICE-73313"
        track_type = (entry.get("metadata") or {}).get("trackType") or "UNKNOWN"

        print(f"v3 MediaID = {mid}")
        if not mid:
            continue

        # Only include Mixed if include_split=False
        if not include_split and track_type.upper() != "MIXED":
            continue

        variants.append({
            "id": vid,   # keep original id for naming
            "mediaId": str(mid),
            "role": track_type.lower(),
            "duration": entry.get("duration", {}).get("durationInMins"),
            "title": (entry.get("analyticsData") or {}).get("content_name") or f"Sleepcast {content_id}",
            "url": f"https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{mid}/download?container=mp3",
        })
    return variants


def extract_sleepcast_variants(info_json: dict, entity_id: str | int) -> list[dict]:
    """
    Sleepcasts expose dailySession.{primaryMediaId, secondaryMediaId} in data.attributes.
    Returns two variants with those media IDs and roles 'ambience' and 'narration'.
    """
    attrs = ((info_json or {}).get("data") or {}).get("attributes") or {}
    ds = attrs.get("dailySession") or {}
    primary = ds.get("primaryMediaId")
    secondary = ds.get("secondaryMediaId")
    name = attrs.get("name")
    content_id = attrs.get("contentId") or attrs.get("id") or entity_id
    dur = _coerce_int(attrs.get("duration"))

    out = []
    def _mk(mid, role):
        if not mid: return None
        return {
            "contentId": content_id,
            "variantId": f"{entity_id}:{role}:{mid}",
            "mediaId": str(mid),
            "role": role,
            "narrator": "narration" if role == "narration" else "ambience",
            "language": None,
            "lengthSec": dur,
            "quality": None,
            "bitrateKbps": None,
            "fileType": "aac",
            "url": None,  # caller fills using HeadspaceService.media_download_url
            "displayName": f"{name} • {role.title()}" if name else f"{role.title()}",
        }
    for role, mid in (("ambience", primary), ("narration", secondary)):
        v = _mk(mid, role)
        if v: out.append(v)
    return out
