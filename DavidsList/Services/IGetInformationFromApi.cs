namespace DavidsList.Services
{
    using DavidsList.Models.API;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    public interface IGetInformationFromApi
    {
        List<MovieQuickShowcaseViewModelWithRaiting> GetMovieShowcaseViewModelWithRaiting();
        Task<List<TopRatedMoviesApiModel>> GetIdsForMostPopularMoviesFromApi();
        Task<List<MovieQuickShowcaseViewModelWithRaiting>> GetMovieDetailsFromApiWithId(List<TopRatedMoviesApiModel> idsOfMovies);
        string CleanUpMoviePath(string path);

    }
}
