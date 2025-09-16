import argparse
import json
import requests
from tqdm import tqdm
from pathlib import Path
from headspace_headers import build_headers
from headspace_variants import fetch_sleepcast_variants_v3

SAVE_DIR = Path("Saved")
AUDIO_DIR = SAVE_DIR / "Audio"

SEED_TOPICS = {
    "SLEEP": [
        {"id": 41, "name": "Sleepcasts", "slug": "sleepcasts"},
        {"id": 89, "name": "Kids and parents", "slug": "kids-and-parents"},
        {"id": 48, "name": "Sleep radio", "slug": "sleep-radio"},
        {"id": 43, "name": "Wind downs", "slug": "wind-downs"},
        {"id": 42, "name": "Sleep music", "slug": "sleep-music"},
        {"id": 86, "name": "Eve's guide to sleep", "slug": "guide-to-sleep"},
        {"id": 44, "name": "Soundscapes", "slug": "soundscapes"},
    ],
    "MEDITATE": [
        {"id": 60, "name": "Timers", "slug": "timers"},
        {"id": 61, "name": "Techniques and support", "slug": "techniques-and-support"},
        {"id": 58, "name": "Courses and singles", "slug": "courses-and-singles"},
    ],
    "FOCUS": [
        {"id": 211, "name": "Binaural Beats Focus", "slug": "binaural-beats-focus"},
        {"id": 176, "name": "White Noise and Sleep Sounds Explore", "slug": "white-noise-and-sleep-sounds-explore"},
        {"id": 62, "name": "Focus Music", "slug": "focus-music"},
        {"id": 65, "name": "Soundscapes", "slug": "soundscapes"},
        {"id": 175, "name": "Sleep Music Focus", "slug": "sleep-music-focus"},
        {"id": 177, "name": "Sleep Radio Focus", "slug": "sleep-radio-focus"},
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
        return None, None

    print(f"\n== {location} Topic ==")
    topic_map = []
    for i, f in enumerate(files, 1):
        topic_id = f.stem.split("_")[-1]
        name = next(
            (t["name"] for t in SEED_TOPICS.get(location, []) if str(t["id"]) == topic_id),
            f"id={topic_id}"
        )
        topic_map.append((f, topic_id, name))
        print(f"  {i}) {name} (id={topic_id})")

    choice = input(f"Choose [1-{len(files)}]: ").strip()
    try:
        return topic_map[int(choice) - 1]
    except:
        return topic_map[0]


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
                    "entityId": attrs.get("entityId"),  # ✅ now captured properly
                    "subtext": attrs.get("subtext"),
                })
    return items

def parse_items_from_viewmodel(data):
    items = []
    # just grab every library-content-tile and pull attributes directly
    for entry in data.get("included", []):
        if entry.get("type") == "library-content-tile":
            attrs = entry.get("attributes", {})
            items.append({
                "category": attrs.get("contentTypeDisplayValue") or "Uncategorized",
                "title": attrs.get("title"),
                "contentId": attrs.get("contentId"),
                "entityId": attrs.get("entityId"),  # ✅ will now be set properly
                "subtext": attrs.get("subtext"),
                "subtextSecondary": attrs.get("subtextSecondary"),
                "bodyText": attrs.get("bodyText"),
            })
    return items

def fetch_variants(entity_id, headers,  content_type=None, args=None):
    if content_type and content_type.upper() == "SLEEPCAST":
        print("Using v3 API for Sleepcast")
        return fetch_sleepcast_variants_v3(entity_id, headers,
                                           date_override=args.sleep_date if args else None,
                                           include_split=args.include_split if args else False)
    else:
        print("Using v1 API for other variation medias")
        url = f"https://api.prod.headspace.com/content/activities?activityGroupIds={entity_id}&limit=100&page=0"
        resp = requests.get(url, headers=headers)
        if not resp.ok:
            print(f"[warn] Failed to fetch variants for entity {entity_id}: {resp.status_code}")
            return []

        data = resp.json()
        included = data.get("included", [])

        # Build mediaItem lookup
        media_lookup = {
            m["id"]: m["attributes"]
            for m in included
            if m.get("type") == "mediaItems"
        }

        variants = []
        for entry in included:
            if entry.get("type") == "activityVariations":
                rel = entry.get("relationships", {}).get("mediaItem", {}).get("data")
                if not rel:
                    continue
                mid = rel.get("id")
                attrs = media_lookup.get(mid)
                if not attrs:
                    continue
                # Ensure this is a real audio file
                if not attrs.get("filename") or not attrs.get("mimeType", "").startswith("audio/"):
                    continue
                variants.append({
                    "id": mid,
                    "filename": attrs.get("filename"),
                    "url": f"https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{mid}/download?container=mp3",
                    "duration": attrs.get("durationInMs"),
                    "codec": attrs.get("audioCodec"),
                    "bitrate": attrs.get("audioBitrateInKbps"),
                })

        return variants


def download_audio(cid, out_path, headers, container="mp3", url = ""):
    if url == "":
        url = f"https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{cid}/download?container={container}"
    else:
        print(f"Using URL Override - {url}")
    
    with requests.get(url, headers=headers, stream=True) as resp:
        resp.raise_for_status()
        if resp.status_code == 401:
            print(f"[err] Unauthorized for {cid}, token expired?")
            return
        total = int(resp.headers.get("content-length", 0))
        out_path.parent.mkdir(parents=True, exist_ok=True)
        with open(out_path, "wb") as f, tqdm(total=total, unit="B", unit_scale=True, desc=out_path.name) as bar:
            for chunk in resp.iter_content(chunk_size=8192):
                if chunk:
                    f.write(chunk)
                    bar.update(len(chunk))


def parse_args():
    ap = argparse.ArgumentParser()
    ap.add_argument("--location", choices=["SLEEP", "MEDITATE", "FOCUS"])
    ap.add_argument("--topic-id")
    ap.add_argument("--container", choices=["mp3", "aac"], default="mp3")
    ap.add_argument("--variant", choices=["auto", "manual"], default="auto")
    ap.add_argument("--sleep-date", help="SLEEPCASTS ONLY - Override date for sleepcast version (YYYY-MM-DD)")
    ap.add_argument("--include-split", action="store_true", help="SLEEPCASTS ONLY - Download voice+ambience+mixed instead of mixed only")
    return ap.parse_args()

def interactive_flow(args):
    location = pick_location()
    if not location:
        return
    vm_file, topic_id, topic_name = pick_viewmodel(location)
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
        print(f"  {i}) {it['title']} ({it['category']}) [cid={it['contentId']} eid={it['entityId']}] {it.get('subtext','')}")
    choice = input(f"Choose [1-{len(items)}]: ").strip()
    try:
        item = items[int(choice) - 1]
    except:
        item = items[0]

    token = load_bearer()
    headers = build_headers(client="Android")
    headers["Authorization"] = f"Bearer {token}"

    title = item.get("title", "audio").replace("/", "-")
    folder = title
    out_dir = AUDIO_DIR / location / folder
    out_dir.mkdir(parents=True, exist_ok=True)

    if item["category"] and item["category"] .upper() == "SLEEPCAST":
            variants = fetch_variants(item["contentId"], headers,
                            content_type=item.get("category") or "None",
                            args=args)
    else:
        variants = fetch_variants(item["entityId"], headers,
                            content_type=item.get("category") or "None",
                            args=args)
    if not variants:
        print("Not varient audio. Downloading directly...")
        out_path = out_dir / f"{title}.mp3"
        download_audio(item["contentId"], out_path, headers)

    if args.variant == "manual":
        print("\n== Variants ==")
        for i, v in enumerate(variants, 1):
            vid = v.get("id")
            fname = v.get("filename", "")
            dur = v.get("duration", "")
            print(f"  {i}) {vid} {fname} {dur}min")
        choice = input(f"Choose [1-{len(variants)}]: ").strip()
        try:
            v = variants[int(choice) - 1]
        except:
            v = variants[0]
        vid = v.get("id")
        out_path = out_dir / f"{title}-{vid}.mp3"
        download_audio(vid, out_path, headers, url=v.get("url"))
    else:
        for v in variants:
            vid = v.get("id")
            narr = v.get("narrator", "")
            dur = v.get("duration", "")
            fname = f"{title}"
            if narr:
                fname += f"-{narr}"
            if dur:
                fname += f"-{dur}min"
            fname += f"-{vid}.mp3"
            out_path = out_dir / fname
            download_audio(vid, out_path, headers, url=v.get("url"))


def main():
    args = parse_args()
    if not args.location or not args.topic_id:
        interactive_flow(args)
    else:
        print("Location or Topic ID Args are missing")


if __name__ == "__main__":
    main()
