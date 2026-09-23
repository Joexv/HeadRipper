# Headripper

**Headripper** is an unofficial toolkit that unlocks the full potential of your Headspace subscription.
It lets you **download, organize, and archive** audio for offline use — something the official app doesn’t make easy.

Instead of hacks or workarounds, Headripper simply mirrors the same API calls that the mobile apps use with your account.
The result: reliable, high-quality audio that you can play on *your terms*.

---

## Features

**Browser Extensions (Chrome/Firefox)**
Add a download button directly inside Headspace’s site to grab whatever audio you’re viewing. Files are auto-organized by title, narrator, and length.

**Python Scripts**
Automate catalog building and batch downloads from the command line. Perfect for pulling whole categories in one go.

**Sleepcast Support (NEW)**
Full integration with Headspace’s newer v3 API.
Download the **Mixed track** (default) or grab all three:

* **Voice** – narration only
* **Ambience** – background soundscape only
* **Mixed** – the final combined track

**Cross-Platform**
Runs on Windows, macOS, and Linux.

**Secure**
Uses your own Bearer token directly with Headspace. Nothing leaves your system except the same requests the Headspace app already makes through your browser.

---

## Why Headripper?

Headspace has a fantastic library — but it’s locked down to mobile devices, with no proper desktop player or open offline access.

Headripper was built to fix that.
It gives paying subscribers the freedom to listen however they want:

* On a desktop at night without needing a phone
* On a work machine with no mobile app installed
* Archived on a personal server or NAS for long-term access

This isn’t for piracy. It’s about access and flexibility for people who already pay.

---

## Versions

### Browser Extensions

Chrome and Firefox add-ons add a small button inside Headspace. Click it to instantly download the current audio.
Files are named and organized automatically.

[Firefox Add-On](https://addons.mozilla.org/en-US/firefox/addon/headripper/?utm_source=joexv.github.io)
Chrome Store: *pending approval*

---

### Python Script

#### Setup

1. Clone this repo and install dependencies:

   ```bash
   pip install -r requirements.txt
   ```
2. Install Playwright:

   ```bash
   python -m playwright install chromium
   ```
3. Install FFmpeg (required for audio processing):

   **Windows:**
   ```bash
   choco install ffmpeg
   ```
   Or with Windows Package Manager:
   ```bash
   winget install ffmpeg
   ```

   **macOS:**
   ```bash
   brew install ffmpeg
   ```

   **Linux (Ubuntu/Debian):**
   ```bash
   sudo apt-get install ffmpeg
   ```

   **Linux (Fedora/RHEL):**
   ```bash
   sudo yum install ffmpeg
   ```

   **Other Linux Distributions:**
   Check your package manager or visit [ffmpeg.org](https://ffmpeg.org/download.html)

4. Run the login helper to capture your Bearer token:

   ```bash
   cd PythonScript
   python Browser_Login.py
   ```

   * A browser will open; log in normally.
   * Your token will be saved to `Saved/BearerID.env`.
   * Tokens usually expire in \~24h, so refresh daily.

---

#### Usage

**Step 1 – Build Catalogs**

```bash
python Headripper.py --location SLEEP --all-topics
```

* `--location` → `SLEEP`, `MEDITATE`, or `FOCUS`
* `--all-topics` → fetch all categories for that location
* Each category is saved as `Saved/viewmodel_<Location>_<TopicID>.json`

Or just fetch one topic:

```bash
python Headripper.py --location SLEEP --topic-id 41
```

---

**Step 2 – Download Audio**

Interactive mode:

```bash
python Download_Audio.py
```

* Shows only the locations & topics you’ve already cached
* Lets you browse and pick tracks
* Saves files to `Saved/Audio/[Location]/[Category]/[Title-Author-Length].mp3`

Batch mode:

```bash
python Download_Audio.py --location SLEEP --topic-id 41 --container mp3
```

This parses the cached viewmodel and downloads all tracks in that topic.

To download one specific item from any cached topic, add `--item`. It accepts either the 1-based number shown by the interactive picker or the exact title, case-insensitively:

```bash
python Download_Audio.py --location SLEEP --topic-id 42 --item 1
python Download_Audio.py --location SLEEP --topic-id 42 --item "Item title"
```

This works for regular audio such as sleep music, wind downs, and soundscapes as well as Sleepcasts. Omit `--sleepcast` for regular audio; it uses the standard variants API.

---

### Sleepcasts

Sleepcasts use a newer API and can contain multiple audio streams.
Headripper supports the **v3 playable-assets API**.

Sleepcasts are selected from the cached Sleepcasts topic like other audio. The `--sleepcast` flag explicitly enables the v3 playable-assets API.

To download one specific sleepcast non-interactively:

```bash
python Download_Audio.py --location sleep --topic-id 41 --item 116
```

For example, these select the same item when item 116 is `Treehouse`:

```bash
python Download_Audio.py --location sleep --topic-id 41 --item 116
python Download_Audio.py --location sleep --topic-id 41 --item "Treehouse"
```

Title matching is case-insensitive. The command downloads the Mixed track by default. Use the interactive mode when you do not know the item number or title:

```bash
python Download_Audio.py
```

Choose `SLEEP`, the `Sleepcasts` topic (topic id `41`), and then the sleepcast you want.

Options:

* `--location` → `SLEEP`, `MEDITATE`, or `FOCUS`; location names are case-insensitive
* `--topic-id 41` → select the cached Sleepcasts topic
* `--item 116` → select one cached sleepcast by its 1-based number or title in quotes; `--title` is retained as an alias
* `--date YYYY-MM-DD` → override the playback date (default: today); `--sleep-date` is an alias
* `--all-tracks` → download VOICE + AMBIENCE + MIXED; `--include-split` is an alias
* `--mixed-only` → download only the Mixed track (default)
* `--variant manual` → choose one returned audio variant interactively

Example outputs:

```
Compass Garden-SC-408-MIXED-73320.mp3
Compass Garden-SC-408-VOICE-73317.mp3
Compass Garden-SC-408-AMBIENCE-73318.mp3
```

Large Sleepcast files (≈50MB each) show a progress bar while downloading.

---

## Automation Workflow

1. **Refresh login** – run `Browser_Login.py` once daily
2. **Update catalogs** – run `Headripper.py` for your locations
3. **Download audio** – run `Download_Audio.py` (interactive or scripted)

---

## Notes

* Sleepcast support is experimental but uses the same headers/auth as the mobile app.
* Android headers are **required** for Sleepcasts.
* If you see `401 Unauthorized`, your token likely expired. Re-run `Browser_Login.py`.
* Viewmodel JSONs are rich with metadata (narrator, durations, etc.) if you want to extend file naming.

---

## Big Kudos

Special thanks to **komali2** — without his original Python script, this project wouldn’t exist.

---

## Disclaimer

Headripper does **not** promote piracy.
Headspace makes valuable content that should be supported.
This project does not let you access anything you aren’t already entitled to.

---

## Bearer ID

Your **BearerID** is the authentication token Headspace uses to confirm your account.
Headripper only uses it locally to make API requests — it is never sent anywhere except directly to Headspace’s servers.

---
