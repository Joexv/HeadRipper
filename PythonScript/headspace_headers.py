from __future__ import annotations

def build_headers(profile: str = "ios", version: str = "303890001", lang: str = "en-US", client="iOS") -> dict:
    profile = (profile or "ios").lower()
    if profile == "ios":
        return {
            "Accept": "application/json",
            "Accept-Language": f"{lang},{lang.split('-')[0]};q=0.9",
            "hs-languagepreference": lang,
            "hs-client-platform": "iOS",
            "hs-client-version": "303890001",
            "User-Agent": f"Headspace%20Mobile/257390 CFNetwork/3860.100.1 Darwin/25.0.0",
            "Origin": "https://my.headspace.com",
            "Referer": "https://my.headspace.com/",
        }
    if profile == "android":
        return{
            "Accept": "application/json",
            "Accept-Language": f"{lang},{lang.split('-')[0]};q=0.9",
            "hs-languagepreference": lang,
            "hs-client-platform": "Android",
            "hs-client-version": "7.82.1",
            "hs-major-version":"7",
            "hs-major-version":"82",
            "User-Agent": "Dart/3.8 (dart:io)",
            "hs-featureflags":"ff_mmbr_d2c_care_android,ff__ij__content__streaming,ff__dynamic_playlist__android,ff__ml_baseline__android,ff__dpl_d1_d10__android,ff__b2b_care_and,ff_mmbr_d2c_care_eligibility_android,ff__mp_profile__android,ff_mp_today_tab_android,ff__mp_explore__android,ff__mv_dpl_ginger__android,ff__mhc_v1__android,ff__dynamic_modes__android,ff__player_up_next_experience,ff__corex_saved,ff__corex_lists"
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
