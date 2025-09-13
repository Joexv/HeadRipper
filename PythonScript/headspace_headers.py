from __future__ import annotations

def build_headers(profile: str = "ios", version: str = "301190000", lang: str = "en-US", client="iOS") -> dict:
    profile = (profile or "ios").lower()
    if profile == "ios":
        return {
            "Accept": "application/json",
            "Accept-Language": f"{lang},{lang.split('-')[0]};q=0.9",
            "hs-languagepreference": lang,
            "hs-client-platform": client,
            "hs-client-version": version,
            "User-Agent": f"Headspace/{version} CFNetwork/1490.0.4 Darwin/23.5.0",
            "Origin": "https://my.headspace.com",
            "Referer": "https://my.headspace.com/",
        }
    return {
        "Accept": "application/json",
        "Accept-Language": f"{lang},{lang.split('-')[0]};q=0.9",
        "hs-languagepreference": lang,
        "User-Agent": ("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 "
                       "(KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36"),
        "Origin": "https://my.headspace.com",
        "Referer": "https://my.headspace.com/",
    }
