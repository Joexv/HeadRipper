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
                File.Delete($"{Name}_{MediaId}.aac");
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
}
