from __future__ import annotations
from typing import Optional

FOCUS_TOPICS_MODULE = "/content-aggregation/v2/topics-module/focus"

#We use iOS headers to mimic the app. Unlocking things like Sleepcasts and other audio categories not found on their website.
#I never migrated this fully into headspace_headers. Oops
DEFAULT_HEADERS = {
    "hs-client-platform": "iOS",
    "hs-client-version": "301190000",
    "hs-languagepreference": "en-US",
    "Accept": "*/*",
    "Accept-Language": "en-US",
    "Origin": "https://my.headspace.com",
    "Referer": "https://my.headspace.com/",
}

def _vm_roots(vm: dict) -> list[dict]:
    if not isinstance(vm, dict):
        return []
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
    """
    Generic mapper for v1 topics-menu view-model:
      -> [{'id', 'name', 'order'}]
    """
    out = []
    roots = _vm_roots(vm)
    for t in roots:
        if not isinstance(t, dict):
            continue
        tid = t.get("topicId") or t.get("id") or t.get("topic_id")
        name = t.get("title") or t.get("name")
        order = t.get("ordinal") or t.get("order") or t.get("sortOrder") or t.get("displayOrder")
        if tid is None:
            continue
        out.append({"id": tid, "name": name, "order": order})
    # stable-ish ordering
    out.sort(key=lambda x: (x.get("order") is None, x.get("order"), str(x.get("name") or "")))
    return out

def map_focus_topics_module(vm: dict) -> list[dict]:
    """
    Mapper for v2 /content-aggregation/v2/topics-module/focus:
      -> [{'id', 'name', 'order'}]
    """
    out = []
    topics = (vm or {}).get("topics") or []
    for i, t in enumerate(topics):
        tid = t.get("id")
        name = t.get("title") or t.get("slug") or f"Topic {tid}"
        if tid is None:
            continue
        out.append({"id": tid, "name": name, "order": i})
    return out

class HeadspaceService:
    def __init__(self, api: "HeadspaceAPI", token: str, *, auth_mode: str, client_profile: str, client_version: str):
        self.api = api
        self.token = token
        self.mode = auth_mode
        self.headers = dict(DEFAULT_HEADERS)
        if client_profile == "ios":
            self.headers["hs-client-platform"] = "IOS" if self.headers.get("hs-client-platform") == "iOS" else "iOS"
            self.headers["hs-client-version"] = client_version
        else:
            self.headers["hs-client-platform"] = "WEB"

    def topics_menu(self, location: str, ttl: int = 3600):
        """
        SLEEP / MEDITATE use v1 topics-menu.
        FOCUS intentionally raises here and should use focus_topics_module().
        """
        if location.upper() == "FOCUS":
            raise RuntimeError("topics-menu not available for FOCUS")
        path = f"/content-aggregation/v1/content/view-models/library/topics-menu?location={location}"
        return self.api.get_json(path, self.token, auth_mode=self.mode, ttl=ttl, headers=self.headers)

    def focus_topics_module(self, user_id: str, ttl: int = 3600):
        """
        FOCUS categories enumerate here (requires userId / hsId).
        """
        path = f"{FOCUS_TOPICS_MODULE}?userId={user_id}"
        return self.api.get_json(path, self.token, auth_mode=self.mode, ttl=ttl, headers=self.headers)

    def topics_category_menu_vm(self, location: str, topic_id: str, *, ttl: int = 0):
        """
        Per-topic “topics-category-menu” view-model (rich items/variants info).
        """
        path = (
            "/content-aggregation/v1/content/view-models/library/topics-category-menu"
            f"?location={location}&tag=&topicId={topic_id}"
        )
        return self.api.get_json(path, self.token, auth_mode=self.mode, ttl=ttl, headers=self.headers)

    def topics_category_menu(self, location: str, topic_id: str, *, ttl: int = 0):
        return self.topics_category_menu_vm(location, topic_id, ttl=ttl)
