namespace DavidsList.Services
{
    using DavidsList.Models.API;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    public interface IGetInformationFromApi
    {
        Task<MovieDetailsViewModel> GetSpecificMovieDetails(string moviePath);
        Task<IEnumerable<MovieQuickShowcaseViewModel>> GetMoviesInParallel_MostPopular();
        Task<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>> GetMoviesInParallel_MostRated();
        string CleanUpMoviePath(string path);

    }
}
