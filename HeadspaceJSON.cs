using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadRipper
{

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

    public class techniqueMedia
    {
        public class Attributes
        {
            public string moduleType { get; set; }
            public object itemCount { get; set; }
            public int ordinalNumber { get; set; }
            public int contentId { get; set; }
            public string entityId { get; set; }

        }

        public class Included
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }

        }

        public class Attributes2
        {
            public int contentId { get; set; }
            public string contentType { get; set; }
            public string entityId { get; set; }
            public bool subscriberContent { get; set; }
            public string animationMediaId { get; set; }

        }

        public class Datum
        {
            public string type { get; set; }
            public string id { get; set; }

        }

        public class ContentInfoModuleDescriptor
        {
            public List<Datum> data { get; set; }

        }

        public class Relationships
        {
            public ContentInfoModuleDescriptor contentInfoModuleDescriptor { get; set; }

        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes2 attributes { get; set; }
            public Relationships relationships { get; set; }

        }

        public class Root
        {
            public List<Included> included { get; set; }
            public Data data { get; set; }

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
            public int? patternMediaId { get; set; }
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
