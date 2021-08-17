namespace DavidsList.Services
{
    using DavidsList.Data;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class DatabaseInteractor : IDatabaseInteractor
    {
        public readonly DavidsListDbContext data;
        public readonly IUserService user;
        public readonly IGetInformationFromApi apiConnector;

        public DatabaseInteractor(DavidsListDbContext dbContext, IUserService userService, IGetInformationFromApi apiInfo)
        {
            this.data = dbContext;
            this.user = userService;
            this.apiConnector = apiInfo;
        }

        public List<MarkedMovieViewModel> GetMarkedMovieViewModel_Favourited()
        {
            var curUsername = user.GetUser().Identity.Name;
            var curUser = data.Users.Include(x=>x.FavouritedMovies).ThenInclude(x=>x.Movie).FirstOrDefault(x => x.UserName == curUsername);
            var result = new List<MarkedMovieViewModel>();
            foreach (var curMovie in curUser.FavouritedMovies)
            {
                result.Add(new MarkedMovieViewModel
                {
                    Title = curMovie.Movie.Title,
                    ImageUrl = curMovie.Movie.Img,
                    MoviePath = curMovie.Movie.MoviePath,
                    ReleaseDate = curMovie.Movie.ReleaseDate,
                    Buttons = apiConnector.CreateButtonModel(curMovie.Movie.MoviePath)
                });
            }
            return result;

        }

        public List<MarkedMovieViewModel> GetMarkedMovieViewModel_Flagged()
        {
            var curUsername = user.GetUser().Identity.Name;
            var curUser = data.Users.Include(x => x.FlaggedMovies).ThenInclude(x => x.Movie).FirstOrDefault(x => x.UserName == curUsername);
            var result = new List<MarkedMovieViewModel>();
            foreach (var curMovie in curUser.FlaggedMovies)
            {
                result.Add(new MarkedMovieViewModel
                {
                    Title = curMovie.Movie.Title,
                    ImageUrl = curMovie.Movie.Img,
                    MoviePath = curMovie.Movie.MoviePath,
                    ReleaseDate = curMovie.Movie.ReleaseDate,
                    Buttons = apiConnector.CreateButtonModel(curMovie.Movie.MoviePath)
                });
            }
            return result;
        }

        public List<MarkedMovieViewModel> GetMarkedMovieViewModel_Seen()
        {
            var curUsername = user.GetUser().Identity.Name;
            var curUser = data.Users.Include(x => x.SeenMovies).ThenInclude(x => x.Movie).FirstOrDefault(x => x.UserName == curUsername);
            var result = new List<MarkedMovieViewModel>();
            foreach (var curMovie in curUser.SeenMovies)
            {
                result.Add(new MarkedMovieViewModel
                {
                    Title = curMovie.Movie.Title,
                    ImageUrl = curMovie.Movie.Img,
                    MoviePath = curMovie.Movie.MoviePath,
                    ReleaseDate = curMovie.Movie.ReleaseDate,
                    Buttons = apiConnector.CreateButtonModel(curMovie.Movie.MoviePath)
                });
            }
            return result;
        }
    }
}
