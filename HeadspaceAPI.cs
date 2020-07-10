using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Threading;
using NAudio.Wave.SampleProviders;
using System.Windows.Forms;
using System.Diagnostics;

namespace HeadRipper
{
    using ps = Properties.Settings;
    class HeadspaceAPI
    {
        //Replace {1} with the desired MediaID
        string aacURL = "https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{1}/download?container=aac";
        string mp3URL = "https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{1}/download?container=mp3";

        //let headers: HTTPHeaders = ["tags" : "", "hs-client-platform" : "iOS", "hs-client-version" : "301030000", "hs-languagepreference" : "en-US", "Authorization" : "bearer \(UserDefaults.standard.string(forKey: "Bearer")!)", "Cookie" : ""]

        //Downloads Audio as AAC
        //curl -v -X GET 'https://api.prod.headspace.com/content-aggregation/v1/content/media-items/{MediaNumber}/download?container=aac&tag=' -H 'Accept: */*' -H 'hs-languagepreference: en-US' -H 'hs-client-version: 301030000' -H 'hs-client-platform: iOS' -H 'Accept-Language: en-us' -H 'Authorization: Bearer {BearerID}' -H 'Accept-Encoding: br, gzip, deflate' -H 'tags: ' -H 'Cookie:'

        //Get Livesession
        //curl -v -X GET 'https://api.prod.headspace.com/livesession/v1/livesessions/{LivesessionID}' -H 'tags: ' -H 'hs-client-platform: iOS' -H 'hs-client-version: 301030000' -H 'hs-languagepreference: en-US' -H 'Authorization: Bearer {BearerID}' -H 'Cookie:'

        //Get Content Data
        //curl -v -X GET 'https://api.prod.headspace.com/content/{category}/{entityId}' -H 'tags: ' -H 'hs-client-version: 301040000' -H 'hs-client-platform: iOS' -H 'hs-languagepreference: en-US' -H 'Authorization: Bearer {BearerID}' -H 'Cookie:'

        //Get Sleep Library Catagories
        //curl -v -X GET 'https://api.prod.headspace.com/content/view-models/library/topics-menu?location=SLEEP' -H 'Accept: */*' -H 'hs-languagepreference: en-US' -H 'hs-client-version: 301040000' -H 'hs-client-platform: iOS' -H 'Accept-Language: en-us' -H 'Authorization: Bearer {BearerID}' -H 'Accept-Encoding: br, gzip, deflate' -H 'tags: ' -H 'Cookie:'

        //Get all Sleepcasts
        //curl -v -X GET 'https://api.prod.headspace.com/content-aggregation/v1/content/view-models/library/topics-category-menu?location=SLEEP&tag=&topicId=41' -H 'Accept: */*' -H 'hs-languagepreference: en-US' -H 'hs-client-version: 301040000' -H 'hs-client-platform: iOS' -H 'Accept-Language: en-us' -H 'Authorization: Bearer {BearerID}}' -H 'Accept-Encoding: br, gzip, deflate' -H 'tags: ' -H 'Cookie:'

        //Get non sleepcast data - very basic data, looks like its sole purpose is to display how big a download will be
        //https://api.prod.headspace.com/content-aggregation/v1/content/view-models/content-info/modules?contentId={0}&moduleType=DOWNLOAD_CONTENT&tag=
        public string Download(string MediaId, string Name)
        {
            if (!File.Exists($"{Name}_{MediaId}.aac"))
            {
                File.Delete($"{Name}_{DateTime.Now.ToString("M_d_yyyy")}.aac");
            }

            //Main Audio File
            var client = new RestClient(aacURL.Replace("{1}", MediaId));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("hs-languagepreference", "en-US");
            request.AddHeader("hs-client-version", "301190000");
            request.AddHeader("hs-client-platform", "iOS");
            request.AddHeader("Accept-Language", "en-us");
            request.AddHeader("Authorization", $"bearer {ps.Default.BearerID}");
            request.AddHeader("Accept-Encoding", "br, gzip, deflate");
            request.AddHeader("tags", "");
            request.AddHeader("Cookie", "");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.StatusCode);
            File.WriteAllBytes($"{Name}_{MediaId}.aac", response.RawBytes);
            response = null;
            client = null;
            return $"{Name}_{MediaId}.aac";
        }

        public string Download(string MediaId, string BackgroundId, string Name, string EpisodeId)
        {
            var client = new RestClient(aacURL.Replace("{1}", MediaId));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("hs-languagepreference", "en-US");
            request.AddHeader("hs-client-version", "301190000");
            request.AddHeader("hs-client-platform", "iOS");
            request.AddHeader("Accept-Language", "en-us");
            request.AddHeader("Authorization", $"bearer {ps.Default.BearerID}");
            request.AddHeader("Accept-Encoding", "br, gzip, deflate");
            request.AddHeader("tags", "");
            request.AddHeader("Cookie", "");

            //Main Audio File. This file should be deleted on completion of the mix, but we will double check just in case it wasn't
            if (!File.Exists($"{Name}_Main_{EpisodeId}.aac"))
            {
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.StatusCode);
                File.WriteAllBytes($"{Name}_Main_{EpisodeId}.aac", response.RawBytes);
                response = null;
            }

            //Background Audio
            if (!File.Exists($"{Name}_Background.aac"))
            {
                client = new RestClient(aacURL.Replace("{1}", BackgroundId));
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.StatusCode);
                File.WriteAllBytes($"{Name}_Background.aac", response.RawBytes);
                response = null;
            }

            client = null;

            ProcessStartInfo ProcessInfo;
            Process Process;
            try
            {
                string FFMPEGcmd = "ffmpeg.exe -i " +
                    $"{Name}_Main_{EpisodeId}.aac -i " +
                    $"{Name}_Background.aac -filter_complex amix=inputs=2:duration=longest " +
                    $"{Name}_mixed_{EpisodeId}.mp3";
                Console.WriteLine(FFMPEGcmd);
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/C cd" + Application.StartupPath + " & " + FFMPEGcmd);
                ProcessInfo.UseShellExecute = true;
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                Process.Dispose();
                File.Delete($"{Name}_Main_{EpisodeId}.aac");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Processing ExecuteCommand : " + e.Message);
            }

            return $"{Name}_mixed_{EpisodeId}.mp3";
        }


        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public void play(string File)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(File);
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();
        }

        public void stop()
        {
            outputDevice.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        public string[] ParseCategories()
        {
            List<String> categories = new List<String>();
            Categories.Root SleepCat = JsonConvert.DeserializeObject<Categories.Root>(GET(@"https://api.prod.headspace.com/content/view-models/library/topics-menu?location=SLEEP"));
            foreach (Categories.Datum datum in SleepCat.data)
            {
                categories.Add(datum.attributes.name + "|" + datum.attributes.location + "|" + datum.attributes.id);
            }
            Console.WriteLine(categories.ToArray());
            return categories.ToArray();
        }

        public Media.Attributes[] ParseMedia(string Category)
        {
            string URL = @"https://api.prod.headspace.com/content-aggregation/v1/content/view-models/library/topics-category-menu?location={0}&tag=&topicId={1}";
            URL = URL.Replace("{0}", Category.Split('|')[1]);
            URL = URL.Replace("{1}", Category.Split('|')[2]);
            List<Media.Attributes> medias = new List<Media.Attributes>();
            string Response = GET(URL);
            Media.Root MediaRoot = JsonConvert.DeserializeObject<Media.Root>(Response);
            Console.WriteLine(Response);
            foreach (Media.Included included in MediaRoot.included)
            {
                medias.Add(included.attributes);
            }
            return medias.ToArray();
        }

        public SleepcastContent.Attributes ParseContent(string Category, string EntityId)
        {
            string URL = @"https://api.prod.headspace.com/content/{0}/{1}";
            URL = URL.Replace("{0}", Category);
            URL = URL.Replace("{1}", EntityId);
            string Response = GET(URL);
            Console.WriteLine("Content Json");
            Console.WriteLine(Response);
            SleepcastContent.Root ContentRoot = JsonConvert.DeserializeObject<SleepcastContent.Root>(Response);
            return ContentRoot.data.attributes;
        }

        //First value in array will always be main media ID
        public List<String> ParseWindDown(string EntityId)
        {
            List<String> IDs = new List<String>();
            string URL = @"https://api.prod.headspace.com/content/activities?activityGroupIds={1}&limit=100&page=0";
            URL = URL.Replace("{1}", EntityId);
            string Response = GET(URL);
            WindDown.Root WDRoot = JsonConvert.DeserializeObject<WindDown.Root>(Response);
            foreach (WindDown.Included included in WDRoot.included)
            {
                if (included.type == "mediaItems")
                    IDs.Add(included.id);
            }

            WindDown.Relationships relation = WDRoot.data[0].relationships;
            foreach (WindDown.Datum3 datum3 in relation.variations.data)
            {
                IDs.Add(datum3.id);
            }
            
            return IDs;
        }

        public String ParseSOS(string EntityId)
        {
            string URL = @"https://api.prod.headspace.com/content/activities?activityGroupIds={1}&limit=100&page=0";
            URL = URL.Replace("{1}", EntityId);
            string Response = GET(URL);
            SOS.Root SOSRoot = JsonConvert.DeserializeObject<SOS.Root>(Response);
            foreach (SOS.Included included in SOSRoot.included)
            {
                if (included.type == "mediaItems")
                    return included.id;
            }
            return "0";
        }

        public String GET(string URL)
        {
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("hs-languagepreference", "en-US");
            request.AddHeader("hs-client-version", "301190000");
            request.AddHeader("hs-client-platform", "iOS");
            request.AddHeader("Accept-Language", "en-us");
            request.AddHeader("Authorization", $"bearer {ps.Default.BearerID}");
            request.AddHeader("Accept-Encoding", "br, gzip, deflate");
            request.AddHeader("tags", "");
            request.AddHeader("Cookie", "");
            IRestResponse response = client.Execute(request);
            client = null;
            return response.Content;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    class Categories
    {
        public class Attributes
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string location { get; set; }
            public string trackingName { get; set; }
            public string backgroundColor { get; set; }
            public string foregroundColor { get; set; }
            public string accentColor { get; set; }
            public int selectorFigureMediaId { get; set; }
            public int headerPatternMediaId { get; set; }
            public int selectorPatternMediaId { get; set; }
            public int ordinalNumber { get; set; }

        }

        public class Datum
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }

        }

        public class Root
        {
            public List<Datum> data { get; set; }

        }
    }

    class Media
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Attributes
        {
            public object id { get; set; }
            public string title { get; set; }
            public string i18nSrcTitle { get; set; }
            public string contentType { get; set; }
            public string contentTypeDisplayValue { get; set; }
            public string labelColorTheme { get; set; }
            public int contentId { get; set; }
            public int entityId { get; set; }
            public string location { get; set; }
            public string trackingName { get; set; }
            public int ordinalNumber { get; set; }
            public string bodyText { get; set; }
            public string subtext { get; set; }
            public string subtextSecondary { get; set; }
            public int imageMediaId { get; set; }
            public int headerImageMediaId { get; set; }
            public bool subscriberContent { get; set; }
            public bool freeToTry { get; set; }
            public string primaryColor { get; set; }
            public string secondaryColor { get; set; }
            public string tertiaryColor { get; set; }
            public int patternMediaId { get; set; }
            public string contentInfoScreenTheme { get; set; }
            public string name { get; set; }
            public string categoryType { get; set; }
            public int? topicMenuId { get; set; }
            public Dictionary<string, int?> DailySession { get; set; }
        }

        public class Datum
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class ContentTiles
        {
            public List<Datum> data { get; set; }

        }

        public class Relationships
        {
            public ContentTiles contentTiles { get; set; }

        }

        public class Included
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }
            public Relationships relationships { get; set; }

        }

        public class Attributes2
        {
            public int id { get; set; }
            public string name { get; set; }
            public string trackingName { get; set; }
            public string description { get; set; }
            public string backgroundColor { get; set; }
            public string foregroundColor { get; set; }
            public string accentColor { get; set; }
            public int headerPatternMediaId { get; set; }

        }

        public class Datum2
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class Categories
        {
            public List<Datum2> data { get; set; }

        }

        public class Relationships2
        {
            public Categories categories { get; set; }

        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes2 attributes { get; set; }
            public Relationships2 relationships { get; set; }

        }

        public class Root
        {
            public List<Included> included { get; set; }
            public Data data { get; set; }

        }


    }

    class SleepcastContent
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class DailySession
        {
            public int primaryMediaId { get; set; }
            public int secondaryMediaId { get; set; }
            public int episodeId { get; set; }

        }

        public class Attributes
        {
            public int id { get; set; }
            public int contentId { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string subtitle { get; set; }
            public int duration { get; set; }
            public bool isEnabled { get; set; }
            public bool isComingSoon { get; set; }
            public bool isSubscriberContent { get; set; }
            public DailySession dailySession { get; set; }
            public int tileBackgroundMediaId { get; set; }
            public int playerBackgroundMediaId { get; set; }

        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }

        }

        public class Root
        {
            public Data data { get; set; }

        }


    }
    
    class SOS
    {
        public class Attributes
        {
            public string text { get; set; }
            public string subtext { get; set; }
            public object title { get; set; }
            public int ordinalNumber { get; set; }
            public string cardType { get; set; }
            public string filename { get; set; }
            public string mimeType { get; set; }
            public int? durationInMs { get; set; }
            public int? audioBitrateInKbps { get; set; }
            public string audioCodec { get; set; }
            public int? fileSizeInBytes { get; set; }
            public string generalType { get; set; }
            public object locale { get; set; }
            public int? showOnCms { get; set; }
            public int? duration { get; set; }

        }

        public class Included
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Datum
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes2 attributes { get; set; }
            public Relationships relationships { get; set; }

        }

        public class Root
        {
            public Meta meta { get; set; }
            public List<Datum> data { get; set; }
            public List<Included> included { get; set; }
        }

        public class Meta
        {
            public int totalpages { get; set; }
        }

        public class Attributes2
        {
            public object privilegeRequirement { get; set; }
            public string enabled { get; set; }
            public string format { get; set; }
            public string name { get; set; }

        }

        public class Datum2
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class Cards
        {
            public List<Datum2> data { get; set; }

        }

        public class Datum3
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class Variations
        {
            public List<Datum3> data { get; set; }

        }

        public class UnlockedActivities
        {
            public List<object> data { get; set; }

        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class PrimaryActivityGroup
        {
            public Data data { get; set; }

        }

        public class Relationships
        {
            public Cards cards { get; set; }
            public Variations variations { get; set; }
            public UnlockedActivities unlockedActivities { get; set; }
            public PrimaryActivityGroup primaryActivityGroup { get; set; }

        }

    }

    class WindDown
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Meta
        {
            public int totalpages { get; set; }

    }

    public class Attributes
    {
        public object privilegeRequirement { get; set; }
        public string enabled { get; set; }
        public string format { get; set; }
        public string name { get; set; }

    }

    public class Datum2
    {
        public string type { get; set; }
        public string id { get; set; }

    }

    public class Cards
    {
        public List<Datum2> data { get; set; }

    }

    public class Datum3
    {
        public string type { get; set; }
        public string id { get; set; }

    }

    public class Variations
    {
        public List<Datum3> data { get; set; }

    }

    public class UnlockedActivities
    {
        public List<object> data { get; set; }

    }

    public class Data
    {
        public string type { get; set; }
        public string id { get; set; }

    }

    public class PrimaryActivityGroup
    {
        public Data data { get; set; }

    }

    public class Relationships
    {
        public Cards cards { get; set; }
        public Variations variations { get; set; }
        public UnlockedActivities unlockedActivities { get; set; }
        public PrimaryActivityGroup primaryActivityGroup { get; set; }

    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }

    }

    public class Attributes2
    {
        public string text { get; set; }
        public string subtext { get; set; }
        public object title { get; set; }
        public int ordinalNumber { get; set; }
        public string cardType { get; set; }
        public string filename { get; set; }
        public string mimeType { get; set; }
        public int? durationInMs { get; set; }
        public int? audioBitrateInKbps { get; set; }
        public string audioCodec { get; set; }
        public int? fileSizeInBytes { get; set; }
        public string generalType { get; set; }
        public object locale { get; set; }
        public int? showOnCms { get; set; }
        public int? duration { get; set; }

    }

    public class Data2
    {
        public string type { get; set; }
        public string id { get; set; }

    }

    public class Activity
    {
        public Data2 data { get; set; }

    }

    public class ImageMedia
    {
        public object data { get; set; }

    }

    public class VideoMedia
    {
        public object data { get; set; }

    }

    public class Data3
    {
        public string type { get; set; }
        public string id { get; set; }

    }

    public class MediaItem
    {
        public Data3 data { get; set; }

    }

    public class Relationships2
    {
        public Activity activity { get; set; }
        public ImageMedia imageMedia { get; set; }
        public VideoMedia videoMedia { get; set; }
        public MediaItem mediaItem { get; set; }

    }

    public class Included
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes2 attributes { get; set; }
        public Relationships2 relationships { get; set; }

    }

    public class Root
    {
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
        public List<Included> included { get; set; }

    }
}
}
