//using NAudio.Wave;
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
using NAudio.Wave;

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
       
        //Get Skeleton Response for certain media. Notably used to get the 'animationMediaId' for the techniques and singles Meditation category
        //Root-Attributes-animationMediaId - Video file ID use with Content View v2 to get information abou the video stored on AmazonAWS server or download it as a m3u8 file
        //https://api.prod.headspace.com/content-aggredgation/v1/content/view-models/content-info/skeleton?contentId={0}&tag=
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
            request.AddHeader("hs-languagepreference", ps.Default.Language.Split('(', ')')[1]);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.StatusCode);
            File.WriteAllBytes($"{Name}_{MediaId}.aac", response.RawBytes);
            response = null;
            client = null;
            return $"{Name}_{MediaId}.aac";
        }

        public string Download(string MediaId, string BackgroundId, string Name, string EpisodeId, bool keepMain, bool keepBackground, bool autoMerge, double Volume = 1.0, double mainVolume = 1.0)
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
            request.AddHeader("hs-languagepreference", ps.Default.Language.Split('(', ')')[1]);

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

            ProcessStartInfo ProcessInfo;
            Process Process;
            if (Volume != 1)
            {
                File.Move($"{Name}_Background.aac", $"{Name}_Background_BeforeAdjust.aac");
                //;\"volume = {Volume}\"
                string FFMPEGcmd = "ffmpeg.exe -i " +
                     $"{Name}_Background_BeforeAdjust.aac -filter_complex \"volume={Volume}\" " +
                     $"{Name}_Background.aac";
                Console.WriteLine(FFMPEGcmd);
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/C cd" + Application.StartupPath + " & " + FFMPEGcmd);
                ProcessInfo.UseShellExecute = true;
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                Process.Dispose();
            }

            if (Volume != 1)
            {
                File.Move($"{Name}_Main_{EpisodeId}.aac", $"{Name}_Main_{EpisodeId}_BeforeAdjust.aac");
                //;\"volume = {Volume}\"
                string FFMPEGcmd = "ffmpeg.exe -i " +
                     $"{Name}_Main_{EpisodeId}_BeforeAdjust.aac -filter_complex \"volume={Volume}\" " +
                     $"{Name}_Main_{EpisodeId}.aac";
                Console.WriteLine(FFMPEGcmd);
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/C cd" + Application.StartupPath + " & " + FFMPEGcmd);
                ProcessInfo.UseShellExecute = true;
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                Process.Dispose();
            }

            client = null;

            try
            {
                if (autoMerge)
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
                }
                if(!keepMain)
                    File.Delete($"{Name}_Main_{EpisodeId}.aac");
                if (!keepBackground)
                    File.Delete($"{Name}_Background_{EpisodeId}.aac");
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
            try
            {
                outputDevice.Stop();
            }
            catch { }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        public string[] ParseCategories(string Category)
        {
            try
            {
                List<String> categories = new List<String>();
                Categories.Root SleepCat = JsonConvert.DeserializeObject<Categories.Root>(GET(@"https://api.prod.headspace.com/content/view-models/library/topics-menu?location={1}".Replace("{1}", Category)));
                if (SleepCat.data == null)
                {
                    MessageBox.Show("Error loading catagories!");
                    return new string[] { "FAILURE" };
                }

                foreach (Categories.Datum datum in SleepCat.data)
                {
                    categories.Add(datum.attributes.name + "|" + datum.attributes.location + "|" + datum.attributes.id);
                }
                Console.WriteLine(categories.ToArray());
                return categories.ToArray();
            }
            catch {
                MessageBox.Show("There was an error loading the categories from Headspace. Double check your BearerID.", "ERROR");
                return new string[] { "FAILURE"};
            }
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
                if(included.attributes.title != string.Empty && included.attributes.entityId != 0)
                    medias.Add(included.attributes);
            }
            return medias.ToArray();
        }

        //Used to get the video Id for Techniques and singles
        public string ParseAnimationId(string ContentId)
        {
            string URL = @"https://api.prod.headspace.com/content-aggregation/v1/content/view-models/content-info/skeleton?contentId={0}&tag=";
            URL = URL.Replace("{0}", ContentId);
            string Response = GET(URL);
            techniqueMedia.Root TRoot = JsonConvert.DeserializeObject<techniqueMedia.Root>(Response);
            return TRoot.data.attributes.animationMediaId;
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
                Console.Write($"\nScanning for {included}\n");
                if (included == null)
                    break;
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
            Console.WriteLine("URL: {0}, \n BearerID: {1}", URL, ps.Default.BearerID);
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("hs-client-version", "301190000");
            request.AddHeader("hs-client-platform", "iOS");
            request.AddHeader("Accept-Language", "en-us");
            request.AddHeader("Authorization", $"Bearer {ps.Default.BearerID}");
            request.AddHeader("Accept-Encoding", "br, gzip, deflate");
            request.AddHeader("tags", "");
            request.AddHeader("Cookie", "");
            request.AddHeader("hs-languagepreference", ps.Default.Language.Split('(', ')')[1]);
            Console.WriteLine(request.Resource);
            IRestResponse response = client.Execute(request);
            client = null;
            //Console.WriteLine(response.Content);
            return response.Content;
        }

    }
}
