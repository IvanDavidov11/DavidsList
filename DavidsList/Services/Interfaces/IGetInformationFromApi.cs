namespace DavidsList.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    using DavidsList.Models;

    public interface IGetInformationFromApi
    {
        Task<MovieDetailsViewModel> GetSpecificMovieDetails(string moviePath);
        Task<IEnumerable<MovieQuickShowcaseViewModel>> GetMoviesInParallel_MostPopular();
        Task<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>> GetMoviesInParallel_MostRated();
        Task<List<SearchResultsViewModel>> GetSearchResultModel(string query);
        string CleanUpMoviePath(string path);
        string GetRandomMoviePath_Surprise();
        string GetRandomMoviePath_Preferred();
        string GetRandomMoviePath_Specific(int genre);
        bool CheckIfUserHasFavouritedGenre();
        Button CreateButtonModel(string movieId);
    }
}
