// content.js — Headripper (baseline + Full Catalog toggle), MP3-only
(function install() {
  if (window.__hsdInstalled) return;
  window.__hsdInstalled = true;

  const TS  = () => new Date().toISOString();
  const log = (...a) => console.log(`[cs ${TS()}]`, ...a);
  const warn= (...a) => console.warn(`[cs ${TS()}]`, ...a);
  const err = (...a) => console.error(`[cs ${TS()}]`, ...a);

  log("content start", location.href);

  const isPlayer = () => /^\/player\//.test(location.pathname);
  const getPlayerParams = () => {
    const url = new URL(location.href);
    const contentId = url.searchParams.get("contentId") || null;
    const authorId  = url.searchParams.get("authorId")  || null;
    const m = location.pathname.match(/\/player\/(\d+)/);
    const activityGroupId = m ? m[1] : null;
    return { contentId, authorId, activityGroupId };
  };

  function noteCurrentContent() {
    const { contentId } = getPlayerParams();
    if (!contentId) return;
    try { chrome.runtime.sendMessage({ type: "noteContent", contentId }, () => void 0); } catch {}
  }

  function readDomTitle() {
    try {
      const h1 = document.querySelector('h1[data-testid="player-title"]');
      if (h1) {
        const spans = Array.from(h1.querySelectorAll("span"));
        const firstText = spans[0]?.textContent?.trim();
        if (firstText) return firstText;
        const text = h1.textContent || "";
        return text.replace(/Audio player\s*$/i, "").trim();
      }
      const og = document.querySelector('meta[property="og:title"]')?.getAttribute("content");
      if (og && og.trim()) return og.trim();
      const t = document.title || "";
      return t.replace(/\|\s*Headspace.*/i, "").trim();
    } catch (_) { return null; }
  }

  // Persisted toggle
  let fullCatalog = false;

  function ensureHost() {
    let host = document.getElementById("hsd-root-host");
    if (host) return host;

    host = document.createElement("div");
    host.id = "hsd-root-host";
    Object.assign(host.style, {
      position: "fixed",
      right: "16px",
      bottom: "16px",
      zIndex: "2147483647",
      pointerEvents: "auto"
    });

    const shadow = host.attachShadow({ mode: "open" });

    const style = document.createElement("style");
    style.textContent = `
      :host { all: initial }
      .wrap { all: initial; display: grid; gap: 8px; min-width: 240px; }
      .row { all: initial; display: flex; align-items: center; gap: 8px; }
      .btn {
        all: initial; display:inline-block; padding: 8px 12px; border-radius: 8px;
        color:#fff; background:#0ea5e9; font:600 13px system-ui,-apple-system,Segoe UI,Roboto,sans-serif;
        cursor:pointer; box-shadow: 0 6px 16px rgba(0,0,0,.18); user-select:none; text-align:center;
      }
      .btn:active { transform: translateY(1px); }
      .card {
        all: initial; display:block; padding:10px; border-radius:12px; background:#ffffffdd; backdrop-filter: blur(6px);
        box-shadow: 0 10px 28px rgba(0,0,0,.22); border: 1px solid rgba(15,23,42,.08);
      }
      .title { all: initial; font:700 12px system-ui,-apple-system,Segoe UI,Roboto,sans-serif; color:#0f172a; letter-spacing:.02em; text-transform:uppercase; opacity:.8; }
      .status { all: initial; font:600 12px system-ui,-apple-system,Segoe UI,Roboto,sans-serif; color:#0f172a; min-height: 16px; }
      label { all: initial; display:flex; align-items:center; gap:8px; font:600 12px system-ui,-apple-system,Segoe UI,Roboto,sans-serif; color:#0f172a; }
      input[type="checkbox"] { all: initial; width: 16px; height: 16px; border-radius: 4px; background: #f1f5f9; border: 1px solid #cbd5e1; cursor: pointer; }
      input[type="checkbox"]:checked { background: #0ea5e9; }
    `;

    const card = document.createElement("div"); card.className = "card";
    const title = document.createElement("div"); title.className = "title"; title.textContent = "Headripper";
    // Row: Download button
    const rowBtn = document.createElement("div"); rowBtn.className = "row";
    const btn = document.createElement("div"); btn.className = "btn"; btn.textContent = "Download";
    rowBtn.append(btn);

    const status = document.createElement("div"); status.className = "status"; status.textContent = "";

    card.append(title, rowBtn, status);
    const wrap = document.createElement("div"); wrap.className = "wrap"; wrap.appendChild(card);
    shadow.append(style, wrap);
    (document.body || document.documentElement).appendChild(host);

    // Initialize checkbox state
    let fullCatalog = false;

    const kick = () => {
      const { contentId, authorId } = getPlayerParams();
      if (!isPlayer() || !contentId) {
        status.textContent = "Open a player page first";
        return;
      }
      noteCurrentContent();

      const domTitle = readDomTitle();
      status.textContent = "Starting…";

      const port = chrome.runtime.connect({ name: "hsd-port" });
      port.onDisconnect.addListener(() => log("port disconnected"));
      port.onMessage.addListener((m) => {
        log("port msg:", m);
        if (m?.status === "progress" && m?.note) status.textContent = m.note;
        if (m?.status === "done") { status.textContent = "Done"; setTimeout(()=> status.textContent="", 1800); }
        if (m?.status === "error") { status.textContent = `Error: ${m.error || "unknown"}`; }
      });
      port.postMessage({
        type: "downloadCurrentVariant",
        payload: { contentId, authorId, container: "mp3", pageTitle: domTitle || document.title || null, fullCatalog }
      });
    };

    btn.addEventListener("click", kick);
    return host;
  }

  function mount() {
    try { ensureHost(); noteCurrentContent(); } catch (e) { err("mount failed:", e); }
  }

  mount();
  window.addEventListener("load", () => { mount(); }, { once: true });
  setInterval(() => { if (!document.getElementById("hsd-root-host")) mount(); }, 2000);

  let lastHref = location.href;
  function onUrlMaybeChanged() {
    if (location.href !== lastHref) { lastHref = location.href; mount(); }
  }
  const _ps = history.pushState;
  const _rs = history.replaceState;
  history.pushState = function(...args) { const r = _ps.apply(this, args); onUrlMaybeChanged(); return r; };
  history.replaceState = function(...args) { const r = _rs.apply(this, args); onUrlMaybeChanged(); return r; };
  window.addEventListener("popstate", onUrlMaybeChanged);

  try {
    chrome.runtime.sendMessage({ type: "ping" }, (res) => {
      const le = chrome.runtime.lastError;
      if (le) err("ping lastError:", le.message);
      else log("ping res:", res);
    });
  } catch (e) { err("ping threw:", e); }
})();
