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
python Headripper.py --location SLEEP --all-topics --client ios
```

Options:

- `--location` can be `SLEEP`, `MEDITATE`, or `FOCUS`.
- `--all-topics` will fetch all known categories for that location.
- Each category is saved into a viewmodel file under `Saved/` (e.g., `viewmodel_SLEEP_41.json`).

You can also specify `--topic-id` to only fetch a single category instead of all of them.

### Step 2 – Download Audio

Once you’ve built catalogs, use the downloader to pick and save audio files.

```bash
python Download_Audio.py
```

This interactive mode will:

- Show you only the locations and topics that have viewmodels available in `Saved/`.
- Let you pick a topic, then list all audio items in that topic.
- Save the chosen track to `Saved/Audio/[Location]/[Category]/[Title-Author-Length].mp3`.

For automation or scripting, you can skip the menus:

```bash
python Download_Audio.py --location SLEEP --topic-id 41 --container mp3
```

This will parse the matching viewmodel file and download all tracks in that topic automatically.

---

## Daily Workflow

Because tokens expire daily, this is the recommended flow:

1. **Refresh login**: Run `Browser_Login.py` once per day to update `BearerID.env`.
2. **Update catalogs**: Run `Headripper.py` for the locations you want (e.g., SLEEP, MEDITATE, FOCUS).
3. **Download audio**: Run `Download_Audio.py` to save tracks interactively, or run it with arguments to batch-download automatically.

---

## Advanced Notes

- If you see `401 Unauthorized`, it almost always means your Bearer token expired. Re-run `Browser_Login.py`.
- `Headripper.py` uses Headspace’s own API endpoints and mirrors their iOS client headers for reliability.
- The downloader script reuses the same authentication and headers, so no extra configuration is needed.
- You can build automation around `Download_Audio.py` by passing arguments in a shell script or scheduled task.
- The viewmodel JSONs are rich: they contain item metadata, durations, narrators, and IDs. You can extend the downloader to include this information in file naming or logs.
- For experimentation, you can run with `--client web` instead of `--client ios`, but `ios` tends to give the most consistent results.



# Disclaimer
By making this program I am not promoting piracy or stealing of content from Headspace. 
They truely believe in what they create and that should be supported for making a service that helps
many many people every day. Nothing in this program allows a person to access content they do not have explicit permissions to access!

# Bearer ID
You will notice in a few places a BearerID mentioned. This is the authentication key that Headspace uses to make sure you are a registered user and you have the rights to the media you are listening to! Headripper will read this BearerID to formulate it's API requests, but it does not track or send this anywhere except Headspace directly.