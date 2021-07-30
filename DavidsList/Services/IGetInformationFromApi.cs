namespace DavidsList.Services
{
    using DavidsList.Models.API;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    public interface IGetInformationFromApi
    {
        //Task<List<MovieQuickShowcaseViewModelWithRaiting>> GetMovieDetailsFromApi_Id_Raiting(List<TopRatedMoviesApiModel> idsOfMovies);
        //Task<List<TopRatedMoviesApiModel>> GetIdsForTopRatedMoviesFromApi();
        List<MovieQuickShowcaseViewModelWithRaiting> GetTopRatedMovieShowcaseViewModel();

        Task<MovieDetailsViewModel> GetSpecificMovieDetails(string moviePath);

        //Task<List<MovieQuickShowcaseViewModel>> GetMovieDetailsFromApi_Id(List<string> idsOfMovies);
        //Task<List<string>> GetIdsForMostPopularMoviesFromApi();
        List<MovieQuickShowcaseViewModel> GetMostPopularMovieShowcaseViewModel();

        string CleanUpMoviePath(string path);

    }
}
