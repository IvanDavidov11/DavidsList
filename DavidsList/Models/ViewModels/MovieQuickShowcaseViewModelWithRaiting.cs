namespace DavidsList.Models.ViewModels
{
    public class MovieQuickShowcaseViewModelWithRaiting
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string MoviePath { get; set; }
        public int Year { get; set; }
        public double Raiting { get; set; }
        public Button Buttons { get; set; }
    }
}
