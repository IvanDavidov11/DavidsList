namespace DavidsList.Models.ViewModels
{
    public class SearchResultsViewModel
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string MoviePath { get; set; }
        public int Year { get; set; }

        public Button Buttons { get; set; }
    }
}
