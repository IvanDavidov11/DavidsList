namespace DavidsList.Models.API.SearchResults
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Image
    {
        public string url { get; set; }
    }
    public class Result
    {
        public string id { get; set; }
        public Image image { get; set; }
        public string title { get; set; }
        public int year { get; set; }
    }

    public class SearchResultsApiModel
    {
        public List<Result> results { get; set; }
    }
}
