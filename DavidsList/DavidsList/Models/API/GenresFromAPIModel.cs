namespace DavidsList.Models.API
{
    using System.Collections.Generic;
    public class Genre
    {
        public string description { get; set; }
        public string endpoint { get; set; }
    }

    public class GenresFromApiModel
    {
        public List<Genre> genres { get; set; }
    }
}
