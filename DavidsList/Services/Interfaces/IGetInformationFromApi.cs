namespace DavidsList.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    public interface IGetInformationFromApi
    {
        Task<MovieDetailsViewModel> GetSpecificMovieDetails(string moviePath);
        Task<IEnumerable<MovieQuickShowcaseViewModel>> GetMoviesInParallel_MostPopular();
        Task<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>> GetMoviesInParallel_MostRated();
        Task<List<SearchResultsViewModel>> GetSearchResultModel(string query);
        string CleanUpMoviePath(string path);
        MovieDetailsViewModel GetRandomMovieModel_Surprise();
        MovieDetailsViewModel GetRandomMovieModel_Preferred();
        MovieDetailsViewModel GetRandomMovieModel_Specific(string genre);
    }
}
