from __future__ import annotations

import argparse, base64, json, os, sys, time
from pathlib import Path

SAVED_DIR = Path("Saved")
ENV_PATH = SAVED_DIR / "BearerID.env"

LOGIN_URL = "https://www.headspace.com/login?redirectOnSuccess=https%3A%2F%2Fmy.headspace.com%2F"
POST_LOGIN_HINT = "https://my.headspace.com/modes/"

def ensure_saved_dir():
    SAVED_DIR.mkdir(parents=True, exist_ok=True)

def _b64url_to_json(seg: str):
    try:
        pad = "=" * (-len(seg) % 4)
        return json.loads(base64.urlsafe_b64decode((seg + pad).encode()).decode("utf-8", "ignore"))
    except Exception:
        return None

def decode_jwt_payload(jwt: str) -> dict | None:
    parts = jwt.split(".")
    if len(parts) < 2:
        return None
    return _b64url_to_json(parts[1])

def install_playwright_if_needed():
    try:
        import playwright  # noqa: F401
        return
    except Exception:
        pass

    # install library
    cmd = [sys.executable, "-m", "pip", "install", "playwright"]
    print("[info] installing playwright via pip...")
    if os.system(" ".join(cmd)) != 0:
        print("[warn] pip install playwright failed. You may need to install manually.")

    # install browser
    print("[info] installing Chromium browser for playwright...")
    os.system(f"{sys.executable} -m playwright install chromium")

def open_and_login(use_creds: bool, save_creds: bool, headless: bool) -> str | None:
    install_playwright_if_needed()
    from playwright.sync_api import sync_playwright, TimeoutError as PWTimeout

    ensure_saved_dir()

    # Optional OS keyring for autofill. Not working
    email = password = None
    service = "Headripper-Headspace"
    try:
        if use_creds:
            import keyring  # type: ignore
            email = keyring.get_password(service, "email") or None
            password = keyring.get_password(service, "password") or None
    except Exception:
        pass

    with sync_playwright() as p:
        browser = p.chromium.launch(headless=headless)
        ctx = browser.new_context()
        page = ctx.new_page()

        print(">>> Please log in normally. This window is a real browser.")
        page.goto(LOGIN_URL, wait_until="domcontentloaded")
        # Autofill if creds exist and fields are present
        if email and password:
            try:
                page.fill('input[name="email"]', email, timeout=1500)
            except Exception:
                try:
                    page.fill('input[type="email"]', email, timeout=1000)
                except Exception:
                    pass
            try:
                page.fill('input[name="password"]', password, timeout=1500)
            except Exception:
                try:
                    page.fill('input[type="password"]', password, timeout=1000)
                except Exception:
                    pass

        # wait for successful navigation under /modes/*
        token = None
        end_time = time.time() + 180  # 3 min window
        last_url = ""
        while time.time() < end_time:
            try:
                page.wait_for_load_state("networkidle", timeout=3000)
            except PWTimeout:
                pass
            cur_url = page.url
            if cur_url != last_url:
                print(f"[info] page url: {cur_url}")
                last_url = cur_url
            if POST_LOGIN_HINT in cur_url or "my.headspace.com/" in cur_url:
                # try to read cookie from context
                cookies = ctx.cookies()
                for c in cookies:
                    if c.get("name") == "hsngjwt":
                        token = c.get("value")
                        break
                if token:
                    break
            time.sleep(0.5)

        if not token:
            # last resort: check document.cookie
            try:
                cookie_str = page.evaluate("document.cookie") or ""
                for kv in cookie_str.split(";"):
                    k, _, v = kv.strip().partition("=")
                    if k == "hsngjwt":
                        token = v
                        break
            except Exception:
                pass

        if not token:
            print("[err] Could not find hsngjwt cookie. Are you logged in?")
            browser.close()
            return None

        # Save env file
        payload = decode_jwt_payload(token) or {}
        exp = payload.get("exp")
        iat = payload.get("iat")
        exp_iso = payload.get("exp_iso") or ""
        iat_iso = payload.get("iat_iso") or ""
        ensure_saved_dir()
        text = f"HSNGJWT={token}\n"
        if exp or iat or exp_iso or iat_iso:
            text += f"# exp={exp} iat={iat} exp_iso={exp_iso} iat_iso={iat_iso}\n"
        ENV_PATH.write_text(text, encoding="utf-8")
        print(f"[ok] Found hsngjwt cookie. Length: {len(token)}")
        print("[done] Token captured and written to Saved/BearerID.env")

        # Optionally store credentials for autofill next time
        if save_creds and email and password:
            try:
                import keyring  # type: ignore
                keyring.set_password(service, "email", email)
                keyring.set_password(service, "password", password)
                print("[ok] Credentials saved to OS keyring for autofill.")
            except Exception:
                print("[warn] Could not save credentials to keyring.")

        browser.close()
        return token

def parse_args():
    ap = argparse.ArgumentParser()
    ap.add_argument("--use-creds", action="store_true", help="Try autofilling saved email/password")
    ap.add_argument("--save-creds", action="store_true", help="Save credentials to OS keyring after login")
    ap.add_argument("--headless", action="store_true", help="Headless browser")
    return ap.parse_args()

if __name__ == "__main__":
    args = parse_args()
    token = open_and_login(use_creds=args.use_creds, save_creds=args.save_creds, headless=args.headless)
    sys.exit(0 if token else 2)
