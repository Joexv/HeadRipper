from __future__ import annotations
import json, os, time, hashlib
from pathlib import Path
from typing import Any, Dict, Optional
import requests

class HeadspaceAPI:
    def __init__(self, api_base: str, cache_dir: Path):
        self.api_base = api_base.rstrip("/")
        self.cache_dir = Path(cache_dir)

    @staticmethod
    def _build_headers(base_headers: Optional[Dict[str, str]], token: str) -> Dict[str, str]:
        h = {"Accept": "application/json"}
        if base_headers:
            h.update(base_headers)
        if token:
            h["Authorization"] = f"Bearer {token}"  # always
        return h

    @staticmethod
    def _maybe_attach_cookie(session: requests.Session, token: str, auth_mode: str):
        if auth_mode == "cookie" and token:
            session.cookies.set("hsngjwt", token, domain=".headspace.com", path="/")

    def _cache_key(self, url: str, headers: Dict[str, str], auth_mode: str) -> str:
        src = f"{url}\n{headers.get('Authorization','')}\nmode={auth_mode}"
        return hashlib.sha256(src.encode("utf-8")).hexdigest()

    def _load_cache(self, key: str, ttl: int):
        p = self.cache_dir / (key + ".json")
        if not p.exists():
            return None
        if ttl > 0 and (time.time() - p.stat().st_mtime) > ttl:
            return None
        try:
            return json.loads(p.read_text(encoding="utf-8"))
        except Exception:
            return None

    def _save_cache(self, key: str, resp: requests.Response, body: Any):
        self.cache_dir.mkdir(parents=True, exist_ok=True)
        (self.cache_dir / (key + ".json")).write_text(
            json.dumps(body, ensure_ascii=False, indent=2), encoding="utf-8"
        )
        meta = {
            "url": resp.url,
            "status": resp.status_code,
            "headers": dict(resp.headers),
            "fetched_at": int(time.time()),
        }
        (self.cache_dir / (key + ".meta.json")).write_text(
            json.dumps(meta, ensure_ascii=False, indent=2), encoding="utf-8"
        )

    def get_json(
        self,
        path: str,
        token: str,
        *,
        auth_mode: str = "bearer",
        ttl: int = 0,
        headers: Optional[Dict[str, str]] = None,
        write_cache: bool = True,
    ) -> Dict[str, Any]:
        url = self.api_base + path
        s = requests.Session()
        self._maybe_attach_cookie(s, token, auth_mode)
        hdrs = self._build_headers(headers, token)

        if os.environ.get("HR_DEBUG_HTTP"):
            print(f"[dbg] GET {url} mode={auth_mode} "
                  f"auth_header={'Authorization' in hdrs} "
                  f"cookie_hsngjwt={'hsngjwt' in [c.name for c in s.cookies]}")

        key = self._cache_key(url, hdrs, auth_mode)
        if ttl > 0:
            cached = self._load_cache(key, ttl)
            if cached is not None:
                return cached

        resp = s.get(url, headers=hdrs, timeout=30)
        if resp.status_code == 401:
            raise RuntimeError("401 Unauthorized")
        if resp.status_code == 404:
            raise RuntimeError("404 Not Found")
        if 500 <= resp.status_code < 600:
            raise RuntimeError(f"{resp.status_code} Server Error")
        resp.raise_for_status()
        body = resp.json()
        if write_cache and ttl > 0:
            self._save_cache(key, resp, body)
        return body
