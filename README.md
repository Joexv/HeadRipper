# HeadRipper
Headspace Downloader

# Why
I'm not an a Headspace user, but I was dissapointed that you could not use a PC or even a web browser to listen to a majority of their library. 
This program will allow me to bring access to those who want to try Headspace but either don't have a mobile device or don't have access to their device during the night.

# Versions

## Browser Extensions
Chrome and Firefox have dedicated extensions that will add a small button to Headspace to download whatever audio you have open. It will organize the files by title, and each file will have the narrator's name and audio length (if applicable)

[Download From FireFox AddOns](https://addons.mozilla.org/en-US/firefox/addon/headripper/?utm_source=joexv.github.io) || Chrome Store Awaiting Approval


# Python Script
## Setup

1. Clone this repository and install dependencies:

   ```bash
   pip install -r requirements.txt
   ```

2. Make sure Playwright is installed:

   ```bash
   python -m playwright install chromium
   ```

3. Run the login helper to capture your Bearer token:

   ```bash
   cd PythonScript
   python Browser_Login.py
   ```

   - A browser will open. Log into Headspace normally.
   - Once logged in, your token will be saved to `Saved/BearerID.env`.
   - Tokens usually expire in about 24 hours, so you’ll need to refresh daily.

---

## Usage

### Step 1 – Build Catalogs

The main script collects category and item data and saves it into local JSON viewmodel files for later use.

```bash
python Headripper.py --location SLEEP --all-topics
```

Options:

- `--location` can be `SLEEP`, `MEDITATE`, or `FOCUS`.
- `--all-topics` will fetch all known categories for that location.
- Each category is saved into a viewmodel file under `Saved/` (e.g., `viewmodel_SLEEP_41.json`).

You can also specify `--topic-id` to only fetch a single category instead of all of them.

#### Step 2 – Download Audio

Once you’ve built catalogs, use the downloader to pick and save audio files.

```bash
python Download_Audio.py
```

This interactive mode will:

* Show you only the locations and topics that have viewmodels available in `Saved/`.
* Let you pick a topic, then list all audio items in that topic.
* Save the chosen track to `Saved/Audio/[Location]/[Category]/[Title-Author-Length].mp3`.

For automation or scripting:

```bash
python Download_Audio.py --location SLEEP --topic-id 41 --container mp3
```

This will parse the matching viewmodel file and download all tracks in that topic automatically.

---

###  Sleepcasts

Sleepcasts are now supported through the **v3 playable-assets API**.
They can provide up to **three separate audio streams** per episode:

* `VOICE` (narration only)
* `AMBIENCE` (background sounds only)
* `MIXED` (final combined track)

By default, only the **MIXED** track is downloaded.
You can change this with new arguments:

```bash
python Download_Audio.py --sleepcast --all-tracks
```

Options:

* `--sleepcast` → Use the v3 playable-assets flow.
* `--date YYYY-MM-DD` → Override the playback date (default: today).
  (Headspace serves different mixes depending on the date.)
* `--all-tracks` → Download VOICE, AMBIENCE, and MIXED.
* `--mixed-only` → Download only the mixed track (default).

Each track is saved with its Name and ID as the filename, e.g.:

```
Compass Garden SC-408-MIXED-73320.mp3
Compass Garden SC-408-VOICE-73317.mp3
Compass Garden SC-408-AMBIENCE-73318.mp3
```

## Daily Workflow

Because tokens expire daily, this is the recommended flow:

1. **Refresh login**: Run `Browser_Login.py` once per day to update `BearerID.env`.
2. **Update catalogs**: Run `Headripper.py` for the locations you want (e.g., SLEEP, MEDITATE, FOCUS).
3. **Download audio**: Run `Download_Audio.py` to save tracks interactively, or run it with arguments to batch-download automatically.

---

## Notes
- I haven't extensively tested varients of audio tracks (length, author, etc) there may be bugs with it but yolo
- Sleepcast support is experimental but uses the same auth + headers as the mobile app.
- If you see `401 Unauthorized`, it almost always means your Bearer token expired. Re-run `Browser_Login.py`.
- Large files (Sleepcasts) now show download progress.
- `Headripper.py` mirrors Android headers for reliability, but Android headers are REQUIRED for Sleepcasts.
- The viewmodel JSONs are rich: you can extend the downloader to include narrator info, durations, etc.

## Big Kudos
to komali2, if he hadn't made that python script I likely never would've come back to this.

# Disclaimer
By making this program I am not promoting piracy or stealing of content from Headspace. 
They truely believe in what they create and that should be supported for making a service that helps
many many people every day. Nothing in this program allows a person to access content they do not have explicit permissions to access!

# Bearer ID
You will notice in a few places a BearerID mentioned. This is the authentication key that Headspace uses to make sure you are a registered user and you have the rights to the media you are listening to! Headripper will read this BearerID to formulate it's API requests, but it does not track or send this anywhere except Headspace directly.
