using Newtonsoft.Json;
using System.Collections.Generic;

public class Image
{
    public int height { get; set; }
    public string id { get; set; }
    public string url { get; set; }
    public int width { get; set; }
}

public class Title
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

public class US
{
    public string certificate { get; set; }
    public int certificateNumber { get; set; }
    public string ratingReason { get; set; }
    public string ratingsBody { get; set; }
    public string country { get; set; }
}

public class Certificates
{
    public List<US> US { get; set; }
}

public class Ratings
{
    public bool canRate { get; set; }
    public double rating { get; set; }
    public int ratingCount { get; set; }
    public int topRank { get; set; }
}

public class PlotOutline
{
    public string author { get; set; }
    public string id { get; set; }
    public string text { get; set; }
}

public class PlotSummary
{
    public string author { get; set; }
    public string id { get; set; }
    public string text { get; set; }
}

public class MovieDetailsApiModel
{
    public string id { get; set; }
    public Title title { get; set; }
    public Certificates certificates { get; set; }
    public Ratings ratings { get; set; }
    public List<string> genres { get; set; }
    public string releaseDate { get; set; }
    public PlotOutline plotOutline { get; set; }
    public PlotSummary plotSummary { get; set; }
}