const API = typeof browser !== 'undefined' ? browser : chrome;
// background.js — Headripper full-catalog toggle, MP3-only, rich filenames
const TS  = () => new Date().toISOString();
const log = (...a) => console.log(`[bg ${TS()}]`, ...a);
const warn= (...a) => console.warn(`[bg ${TS()}]`, ...a);
const err = (...a) => console.error(`[bg ${TS()}]`, ...a);

const ORIGINS = [
  "https://api.prod.headspace.com/*",
  "https://www.headspace.com/*",
  "https://my.headspace.com/*",
  "*://*.cloudfront.net/*"
];

const HS_BASE = "https://api.prod.headspace.com";
const IOS_HEADERS = {
  "hs-languagepreference": "en-US",
  "hs-client-version": "301040000",
  "hs-client-platform": "iOS",
  "Accept-Language": "en-us",
  "Accept-Encoding": "br, gzip, deflate",
  "tags": "",
  "Cookie": ""
};

let bearer = null;
const tabContent = new Map(); // tabId -> contentId
// contentId -> Map(mediaId -> { lastSeen, lastContainer, signedByContainer:{}, meta? })
const learnedByContent = new Map();

log("Service worker starting…");
API.runtime.onInstalled.addListener(d=>{ log("onInstalled:", d); ensureHostPermissions(); });
API.runtime.onStartup?.addListener(()=>{ log("onStartup"); ensureHostPermissions(); });

try {
  API.alarms.create("heartbeat", { periodInMinutes: 1 });
  API.alarms.onAlarm.addListener(a => { if (a?.name === "heartbeat") log("heartbeat"); });
  log("alarms heartbeat installed");
} catch (e) { err("heartbeat setup failed:", e); }

API.storage.session.get(["bearer"]).then(st => {
  bearer = st.bearer || null;
  log("restored bearer?", !!bearer);
});

async function ensureHostPermissions() {
  try {
    const have = await API.permissions.contains({ origins: ORIGINS });
    log("permissions.contains origins:", have);
    if (!have) {
      const granted = await API.permissions.request({ origins: ORIGINS });
      log("permissions.request origins granted:", granted);
      if (!granted) warn("Host permissions not granted; webRequest may not fire.");
    }
  } catch (e) { err("ensureHostPermissions error:", e); }
}

async function getBearer() {
  if (bearer) return bearer;
  const st = await API.storage.session.get(["bearer"]);
  bearer = st.bearer || null;
  return bearer;
}

// ——— sniff bearer + current media variant
API.webRequest.onSendHeaders.addListener(
  async (details) => {
    const { url, method, tabId } = details;
    if (!url.startsWith(HS_BASE)) return;

    for (const h of details.requestHeaders || []) {
      if (h.name.toLowerCase() === "authorization") {
        const m = /^bearer\s+(.+)$/i.exec(h.value || "");
        if (m) {
          const tok = m[1].trim();
          if (tok && tok !== bearer) {
            bearer = tok;
            API.storage.session.set({ bearer });
            log("Captured new bearer", { method, url, tabId });
          }
        }
        break;
      }
    }

    const m = url.match(/\/content(?:-aggregation\/v1\/content)?\/media-items\/(\d+)\/download\?[^#]*container=([a-z0-9]+)/i);
    if (m && tabId >= 0) {
      const mediaId = m[1];
      const container = m[2].toLowerCase();
      const cid = await inferOrGetContentId(tabId);
      if (!cid) return;

      let map = learnedByContent.get(cid);
      if (!map) { map = new Map(); learnedByContent.set(cid, map); }
      let entry = map.get(mediaId);
      if (!entry) entry = { lastSeen: 0, lastContainer: null, signedByContainer: {}, meta: {} };
      entry.lastSeen = Date.now();
      entry.lastContainer = container;
      map.set(mediaId, entry);

      log("learned media from traffic", { contentId: cid, mediaId, container });
    }
  },
  { urls: ["https://api.prod.headspace.com/*"] },
  ["requestHeaders"]
);

API.webRequest.onBeforeRedirect.addListener(
  async (details) => {
    const { url, redirectUrl, tabId } = details;
    if (!redirectUrl) return;
    const m = url.match(/\/content(?:-aggregation\/v1\/content)?\/media-items\/(\d+)\/download\?[^#]*container=([a-z0-9]+)/i);
    if (!m) return;

    const mediaId = m[1];
    const container = m[2].toLowerCase();
    const cid = await inferOrGetContentId(tabId);
    if (!cid) return;

    let map = learnedByContent.get(cid);
    if (!map) { map = new Map(); learnedByContent.set(cid, map); }
    let entry = map.get(mediaId);
    if (!entry) entry = { lastSeen: 0, lastContainer: null, signedByContainer: {}, meta: {} };
    entry.lastSeen = Date.now();
    entry.signedByContainer[container] = redirectUrl;
    map.set(mediaId, entry);

    log("learned signed URL", { contentId: cid, mediaId, container, redirectUrl });
  },
  { urls: ["https://api.prod.headspace.com/*"] }
);

API.webRequest.onCompleted.addListener(
  (d) => log("API completed:", d.statusCode, d.method, d.url),
  { urls: ["https://api.prod.headspace.com/*"] }
);

// ——— fetch helpers
async function hsFetchJson(path) {
  const tok = await getBearer();
  if (!tok) throw new Error("Not authenticated yet — use the site first.");
  const url = `${HS_BASE}${path}`;
  const resp = await fetch(url, {
    headers: { ...IOS_HEADERS, "Authorization": `Bearer ${tok}`, "Accept": "application/json" },
    cache: "no-store", redirect: "follow"
  });
  if (!resp.ok) {
    const body = await resp.text().catch(()=>"<read error>");
    err("hsFetchJson fail", resp.status, url, body.slice(0,200));
    throw new Error(`HTTP ${resp.status} for ${path}`);
  }
  return resp.json();
}

async function resolveSignedUrl(path) {
  const tok = await getBearer();
  if (!tok) throw new Error("Not authenticated yet.");
  const url = `${HS_BASE}${path}`;
  let resp = await fetch(url, {
    headers: { ...IOS_HEADERS, "Authorization": `Bearer ${tok}` },
    redirect: "follow", cache: "no-store"
  });
  if (resp.url && !/^https:\/\/api\.prod\.headspace\.com/i.test(resp.url)) return resp.url;

  const man = await fetch(url, {
    headers: { ...IOS_HEADERS, "Authorization": `Bearer ${tok}` },
    redirect: "manual", cache: "no-store"
  });
  const loc = man.headers.get("Location");
  if (!loc) throw new Error("No redirect Location from Headspace.");
  return loc;
}

function safeBaseName(s) {
  return (s || "").toString().replace(/[^\w\s-]+/g, "").trim().replace(/\s+/g, " ");
}

async function inferOrGetContentId(tabId) {
  if (tabContent.has(tabId)) return tabContent.get(tabId);
  try {
    const t = await API.tabs.get(tabId);
    const u = new URL(t.url || "");
    const cid = u.searchParams.get("contentId");
    if (cid) { tabContent.set(tabId, cid); return cid; }
  } catch {}
  return null;
}

// ——— playable-assets meta
async function seedPlayableAssetsMeta(contentId) {
  try {
    const items = await hsFetchJson(`/content-interface/v1/playable-assets?audioDescriptionEnabled=false&contentId=${encodeURIComponent(contentId)}`);
    if (!items || !Array.isArray(items)) return false;

    let map = learnedByContent.get(String(contentId));
    if (!map) { map = new Map(); learnedByContent.set(String(contentId), map); }

    for (const it of items) {
      const mediaId = (it?.mediaItemId != null) ? String(it.mediaItemId) : null;
      if (!mediaId) continue;

      const durationMins = it?.duration?.durationInMins ?? null;
      const speakerRaw  = it?.narrator?.displayName || it?.analyticsData?.voice || null;
      const titleRaw    = it?.analyticsData?.content_name || it?.analyticsData?.course_name || null;
      const mediaName   = it?.analyticsData?.media_name || null;

      let entry = map.get(mediaId);
      if (!entry) entry = { lastSeen: 0, lastContainer: null, signedByContainer: {}, meta: {} };

      entry.meta = {
        ...(entry.meta || {}),
        title: titleRaw || entry.meta?.title || null,
        speaker: speakerRaw || entry.meta?.speaker || null,
        durationMins: durationMins || entry.meta?.durationMins || null,
        mediaName: mediaName || entry.meta?.mediaName || null
      };

      map.set(mediaId, entry);
    }

    log("seedPlayableAssetsMeta ok", { contentId, count: learnedByContent.get(String(contentId))?.size || 0 });
    return true;
  } catch (e) {
    warn("seedPlayableAssetsMeta fail:", e?.message || e);
    return false;
  }
}

// ——— split sleepcast details
async function hsGetSleepcastSplit(entityId) {
  const json = await hsFetchJson(`/content/sleepcasts/${encodeURIComponent(entityId)}`);
  const a = json?.data?.attributes || {};
  const ds = a?.dailySession || {};
  const out = {
    entityId: String(entityId),
    title: a?.name || null,
    ambienceMediaId: ds?.primaryMediaId || null,
    narrationMediaId: ds?.secondaryMediaId || null
  };
  return out;
}

async function hsSignedAudioUrlMP3(mediaItemId) {
  const path = `/content-aggregation/v1/content/media-items/${encodeURIComponent(mediaItemId)}/download?container=mp3&tag=`;
  return resolveSignedUrl(path);
}

// ——— title + speaker fallbacks
async function resolveTitle(contentId, authorId) {
  try {
    const mod = await hsFetchJson(`/content-aggregation/v2/content/view-models/content-info/modules?contentId=${encodeURIComponent(contentId)}&moduleType=PAGE_HEADER`);
    const name = mod?.data?.attributes?.title || mod?.data?.attributes?.name
      || mod?.data?.attributes?.header?.title;
    if (name) return name.toString();
  } catch {}
  try {
    const act = await hsFetchJson(`/content/v1/activities/${encodeURIComponent(contentId)}${authorId ? `?authorId=${encodeURIComponent(authorId)}` : ""}`);
    const name = act?.data?.attributes?.name || act?.data?.attributes?.title;
    if (name) return name.toString();
  } catch {}
  return `Content ${contentId}`;
}

async function resolveSpeakerFallback(contentId) {
  try {
    const sel = await hsFetchJson(`/content-interface/v1/content-info-select-narrator?contentId=${encodeURIComponent(contentId)}`);
    const opts = sel?.data?.attributes?.options || sel?.options || [];
    const label = opts[0]?.label || opts[0]?.name || opts[0]?.title;
    if (label) return String(label);
  } catch {}
  return null;
}

// ——— pick most recent variant only
function pickMostRecentVariant(learnedMap, preferredContainer) {
  let best = null;
  let bestKey = null;
  for (const [mediaId, entry] of learnedMap.entries()) {
    if (!best || (entry.lastSeen || 0) > (best.lastSeen || 0)) {
      best = entry; bestKey = mediaId;
    }
  }
  if (!bestKey) return null;
  const signedUrl = best.signedByContainer?.[preferredContainer] || null;
  return { mediaId: bestKey, signedUrl, meta: best.meta || {} };
}

// ——— single-variant downloader
async function downloadSingleVariant(payload, say) {
  const { contentId, authorId, container, pageTitle } = payload;
  await seedPlayableAssetsMeta(contentId);

  for (let i = 0; i < 4; i++) {
    const map = learnedByContent.get(String(contentId));
    if (map && map.size) break;
    await new Promise(r => setTimeout(r, 200));
  }

  const learnedMap = learnedByContent.get(String(contentId));
  if (!learnedMap || !learnedMap.size) throw new Error("Couldn’t detect a variant. Hit Play once, then try again.");

  const pick = pickMostRecentVariant(learnedMap, container);
  if (!pick) throw new Error("No playable variant found.");
  const { mediaId, signedUrl, meta } = pick;

  const title = safeBaseName(
    (pageTitle && pageTitle.trim())
      || meta.title
      || await resolveTitle(contentId, authorId)
      || `Content ${contentId}`
  );
  const speaker = safeBaseName(meta.speaker || await resolveSpeakerFallback(contentId) || "");
  const mins = meta.durationMins && Number.isFinite(meta.durationMins) ? Math.round(meta.durationMins) : null;
  const base = speaker ? `${title} - ${speaker}` : title;
  const name = mins ? `${base} - ${mins}m` : base;

  const url = signedUrl || await hsSignedAudioUrlMP3(mediaId);
  const filename = `${title}/${name}.mp3`;

  await new Promise((resolve, reject) => {
    API.downloads.download({ url, filename, saveAs: false }, (id) => {
      const le = API.runtime.lastError;
      if (le) reject(new Error(le.message));
      else resolve(id);
    });
  });
}

// ——— split downloader if available
async function downloadSplitIfAvailable(entityId, pageTitle, say) {
  const info = await hsGetSleepcastSplit(entityId).catch(() => null);
  if (!info) return false;
  const baseTitle = safeBaseName(info.title || pageTitle || `Sleepcast ${entityId}`);

  const parts = [];
  if (info.ambienceMediaId) parts.push({ name: "Ambience", mediaId: info.ambienceMediaId });
  if (info.narrationMediaId) parts.push({ name: "Narration", mediaId: info.narrationMediaId });
  if (!parts.length) return false;

  let completed = 0;
  for (const p of parts) {
    say?.({ status: "progress", note: `Preparing ${p.name}…` });
    const url = await hsSignedAudioUrlMP3(p.mediaId);
    const fileBase = `${baseTitle} - ${p.name}`;
    const filename = `${baseTitle}/${fileBase}.mp3`;
    await new Promise((resolve, reject) => {
      API.downloads.download({ url, filename, saveAs: false }, (id) => {
        const le = API.runtime.lastError;
        if (le) reject(new Error(le.message));
        else resolve(id);
      });
    });
    completed += 1;
    say?.({ status: "progress", note: `Downloaded ${completed}/${parts.length}…` });
  }
  return true;
}

// ——— main task
async function doDownloadCurrentVariant(payload, say) {
  let { contentId, authorId, container, pageTitle, fullCatalog } = payload;
  container = "mp3"; // force
  const entityId = contentId;

  if (fullCatalog) {
    say?.({ status: "progress", note: "Checking full catalog…" });
    const ok = await downloadSplitIfAvailable(entityId, pageTitle, say).catch(()=>false);
    if (ok) { say?.({ status: "done" }); return; }
    warn("Full catalog: no split media; falling back to single variant");
  }

  say?.({ status: "progress", note: "Resolving selection…" });
  await downloadSingleVariant({ contentId, authorId, container, pageTitle }, say);
  say?.({ status: "done" });
}

// —— messaging
API.runtime.onMessage.addListener((msg, sender, sendResponse) => {
  if (msg?.type === "ping") {
    API.permissions.contains({ origins: ORIGINS })
      .then(havePerms => sendResponse({ ok: true, where: "background", bearerKnown: !!bearer, havePerms }))
      .catch(e => { err("ping error", e); sendResponse({ ok:false, error:String(e) }); });
    return true;
  }
  if (msg?.type === "noteContent" && sender?.tab?.id >= 0) {
    const { contentId } = msg;
    if (contentId) {
      tabContent.set(sender.tab.id, String(contentId));
      log("noteContent", { tabId: sender.tab.id, contentId });
    }
    sendResponse({ ok: true });
    return false;
  }
  return false;
});

// —— Port pipeline
API.runtime.onConnect.addListener((port) => {
  log("onConnect", port.name);
  if (port.name !== "hsd-port") return;

  port.onMessage.addListener(async (msg) => {
    log("port onMessage", msg?.type);
    const say = (m) => { try { port.postMessage(m); } catch (e) { err("port postMessage failed:", e); } };

    try {
      await ensureHostPermissions();

      if (msg?.type === "downloadCurrentVariant") {
        await doDownloadCurrentVariant(msg.payload, say);
        try { port.disconnect(); } catch {}
        return;
      }

      say({ status: "error", error: "unknown message" });
      try { port.disconnect(); } catch {}
    } catch (e) {
      err("port task error:", e);
      say({ status: "error", error: e?.message || String(e) });
      try { port.disconnect(); } catch {}
    }
  });
});
