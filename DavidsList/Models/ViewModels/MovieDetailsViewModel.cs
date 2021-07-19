namespace DavidsList.Models.ViewModels
{
    using System.Collections.Generic;
    public class MovieDetailsViewModel
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string MoviePath { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public string ReleaseDate { get; set; }

        public double Raiting { get; set; }
        public int RaitingCount { get; set; }

        public List<string> Genres { get; set; }

        public string ShortPlot { get; set; }
        public string LongPlot { get; set; }
    }
}
