namespace DavidsList.Models.API
{
    using Newtonsoft.Json;
    public class MovieQuickShowcaseJsonModel
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public Image image { get; set; }
        public int runningTimeInMinutes { get; set; }
        public string title { get; set; }
        public string titleType { get; set; }
        public int year { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }



}
