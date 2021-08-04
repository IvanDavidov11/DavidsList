using Newtonsoft.Json;
using System.Collections.Generic;

public class Image
{
    public string url { get; set; }
}

public class Title
{
    public Image image { get; set; }
    public int runningTimeInMinutes { get; set; }
    public string title { get; set; }
}


public class Ratings
{
    public double rating { get; set; }
    public int ratingCount { get; set; }
}

public class PlotOutline
{
    public string text { get; set; }
}

public class PlotSummary
{
    public string text { get; set; }
}

public class MovieDetailsApiModel
{
    public Title title { get; set; }
    public Ratings ratings { get; set; }
    public List<string> genres { get; set; }
    public string releaseDate { get; set; }
    public PlotOutline plotOutline { get; set; }
    public PlotSummary plotSummary { get; set; }
}