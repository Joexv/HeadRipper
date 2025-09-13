import argparse
import json
import requests
from pathlib import Path
from headspace_headers import build_headers
SAVE_DIR = Path("Saved")
AUDIO_DIR = SAVE_DIR / "Audio"
BASE_URL = "https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{cid}/download?container={container}"

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
        {"id": 211, "name": "Binaural Beats Focus"},
        {"id": 176, "name": "White Noise and Sleep Sounds Explore"},
        {"id": 62, "name": "Focus Music"},
        {"id": 65, "name": "Soundscapes"},
        {"id": 175, "name": "Sleep Music Focus"},
        {"id": 177, "name": "Sleep Radio Focus"},
    ],
}


def load_bearer():
    env_file = SAVE_DIR / "BearerID.env"
    if env_file.exists():
        with open(env_file, encoding="utf-8") as f:
            for line in f:
                if line.startswith("HSNGJWT="):
                    return line.strip().split("=", 1)[1]
    print("[err] Missing or invalid BearerID.env")
    return None


def list_locations():
    found = set()
    for f in SAVE_DIR.glob("viewmodel_*.json"):
        parts = f.stem.split("_")
        if len(parts) >= 2:
            found.add(parts[1])
    return sorted(found)


def pick_location():
    locs = list_locations()
    if not locs:
        print("No viewmodels found in Saved/. Run Headripper.py first.")
        return None
    print("\n== Pick Category ==")
    for i, loc in enumerate(locs, 1):
        print(f"  {i}) {loc}")
    choice = input(f"Choose [1-{len(locs)}] default 1: ").strip()
    if not choice:
        return locs[0]
    try:
        return locs[int(choice) - 1]
    except:
        return locs[0]


def load_viewmodels(location):
    return list(SAVE_DIR.glob(f"viewmodel_{location}_*.json"))


def pick_viewmodel(location):
    files = load_viewmodels(location)
    if not files:
        print(f"No viewmodels found for {location}. Run Headripper.py first.")
        return None
    print(f"\n== {location} Topic ==")
    for i, f in enumerate(files, 1):
        topic_id = f.stem.split("_")[-1]
        name = next((t["name"] for t in SEED_TOPICS.get(location, []) if str(t["id"]) == topic_id), f"id={topic_id}")
        print(f"  {i}) {name} (id={topic_id})")
    choice = input(f"Choose [1-{len(files)}]: ").strip()
    try:
        return files[int(choice) - 1]
    except:
        return files[0]


def parse_items_from_viewmodel(data):
    items = []
    tiles = {c["id"]: c["attributes"] for c in data.get("included", []) if c.get("type") == "library-content-tile"}
    categories = [c for c in data.get("included", []) if c.get("type") == "library-topic-category"]
    for cat in categories:
        cat_name = cat.get("attributes", {}).get("title")
        rels = cat.get("relationships", {}).get("contentTiles", {}).get("data", [])
        for ref in rels:
            tid = ref.get("id")
            if tid in tiles:
                attrs = tiles[tid]
                items.append({
                    "category": cat_name,
                    "title": attrs.get("title"),
                    "contentId": attrs.get("contentId"),
                    "subtext": attrs.get("subtext"),
                })
    return items


def download_audio(cid, out_path, container="mp3"):
    token = load_bearer()
    if not token:
        print("[err] Cannot download without Bearer token.")
        return

    headers = build_headers(client="ios")
    headers["Authorization"] = f"Bearer {token}"

    url = BASE_URL.format(cid=cid, container=container)
    out_path.parent.mkdir(parents=True, exist_ok=True)

    print("\n[dbg] About to request:")
    print(f"URL: {url}")
    for k, v in headers.items():
        if k.lower() == "authorization":
            print(f"  {k}: Bearer <redacted token len={len(v.split()[-1])}>")

    print(f"[info] Downloading {cid} -> {out_path}")
    with requests.get(url, headers=headers, stream=True) as r:
        print(f"[dbg] Response status: {r.status_code}")
        if(r.status_code == 401):
            print("[err] 401. Likely invalid Bearer token. Relogin and try again.")
            return
        if(r.status_code == 404):
            print("[err] 404. ContentId not found.")
            return
        # print(f"[dbg] Response headers: {dict(r.headers)}")
        r.raise_for_status()
        with open(out_path, "wb") as f:
            for chunk in r.iter_content(chunk_size=8192):
                f.write(chunk)
    print("[ok] Done")


def parse_args():
    ap = argparse.ArgumentParser()
    ap.add_argument("--location", choices=["SLEEP", "MEDITATE", "FOCUS"])
    ap.add_argument("--topic-id")
    ap.add_argument("--container", choices=["mp3", "aac"], default="mp3")
    return ap.parse_args()


def interactive_flow():
    location = pick_location()
    if not location:
        return
    vm_file = pick_viewmodel(location)
    if not vm_file:
        return
    with open(vm_file, encoding="utf-8") as f:
        data = json.load(f)
    items = parse_items_from_viewmodel(data)
    if not items:
        print("No items found in this topic.")
        return
    print("\n== Pick Audio ==")
    for i, it in enumerate(items, 1):
        print(f"  {i}) {it['title']} ({it['category']}) [id={it['contentId']}] {it.get('subtext','')}")
    choice = input(f"Choose [1-{len(items)}]: ").strip()
    try:
        item = items[int(choice) - 1]
    except:
        item = items[0]
    title = item.get("title", "audio").replace("/", "-")
    cid = item.get("contentId")
    out_path = AUDIO_DIR / location / f"{title}.mp3"
    download_audio(cid, out_path)


def direct_flow(args):
    vm_file = SAVE_DIR / f"viewmodel_{args.location}_{args.topic_id}.json"
    if not vm_file.exists():
        print(f"Missing {vm_file}. Run Headripper.py first.")
        return
    with open(vm_file, encoding="utf-8") as f:
        data = json.load(f)
    items = parse_items_from_viewmodel(data)
    if not items:
        print("No items found in this topic.")
        return
    for it in items:
        title = it.get("title", "audio").replace("/", "-")
        cid = it.get("contentId")
        out_path = AUDIO_DIR / args.location / f"{title}.{args.container}"
        download_audio(cid, out_path, args.container)


def main():
    args = parse_args()
    if not args.location or not args.topic_id:
        interactive_flow()
    else:
        direct_flow(args)


if __name__ == "__main__":
    main()