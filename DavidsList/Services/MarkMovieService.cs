namespace DavidsList.Services
{
    using System;
    using System.Linq;
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using Microsoft.EntityFrameworkCore;
    using DavidsList.Services.Interfaces;
    using DavidsList.Data.DbModels.ManyToManyTables;
    using AspNetCoreHero.ToastNotification.Abstractions;

    public class MarkMovieService : IMarkMovieService
    {
        private readonly DavidsListDbContext data;
        private readonly INotyfService _notyf;
        private readonly IGetInformationFromApi apiConnector;

        public MarkMovieService(DavidsListDbContext db, INotyfService notyf, IGetInformationFromApi apiInf)
        {
            this.data = db;
            this._notyf = notyf;
            this.apiConnector = apiInf;
        }

        public void MarkMovieAsDisliked(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.DislikedMovies).Include(x => x.LikedMovies).FirstOrDefault(x => x.UserName == userName);
            CreateMovieIfItDoesNotExist(movieId);
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            if (CheckIfMovieIsAlready_Liked(curUser, movie))
            {
                RemoveLikedMovie(curUser, movie);
                _notyf.Success("Successfuly removed Like from movie...");

            }

            if (CheckIfMovieIsAlready_Disliked(curUser, movie))
            {
                RemoveDislikedMovie(curUser, movie);
                _notyf.Success("Successfuly removed Dislike from movie...");
            }
            else
            {
                data.DislikedMovies.Add(new DislikedMovie
                {
                    MovieId = movie.Id,
                    UserId = curUser.Id
                });
                _notyf.Success("Successfuly marked movie as Disliked...");
            }
            data.SaveChanges();
        }

        public void MarkMovieAsFavourite(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.FavouritedMovies).FirstOrDefault(x => x.UserName == userName);
            CreateMovieIfItDoesNotExist(movieId);
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            if (CheckIfMovieIsAlready_Favourited(curUser, movie))
            {
                RemoveFavouritedMovie(curUser, movie);
                _notyf.Success("Successfuly removed movie from Favourites...");

            }
            else
            {
                data.FavouritedMovies.Add(new FavouritedMovie
                {
                    MovieId = movie.Id,
                    UserId = curUser.Id
                });
                _notyf.Success("Successfuly marked movie as Favourite...");

            }
            data.SaveChanges();
        }

        public void MarkMovieAsFlagged(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.FlaggedMovies).FirstOrDefault(x => x.UserName == userName);
            CreateMovieIfItDoesNotExist(movieId);
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            if (CheckIfMovieIsAlready_Flagged(curUser, movie))
            {
                RemoveFlaggedMovie(curUser, movie);
                _notyf.Success("Successfuly removed Flagg from movie...");

            }
            else
            {
                data.FlaggedMovies.Add(new FlaggedMovie
                {
                    MovieId = movie.Id,
                    UserId = curUser.Id
                });
                _notyf.Success("Successfuly marked movie as Flagged...");
            }
            data.SaveChanges();
        }

        public void MarkMovieAsLiked(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.LikedMovies).Include(x => x.DislikedMovies).FirstOrDefault(x => x.UserName == userName);
            CreateMovieIfItDoesNotExist(movieId);
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            if (CheckIfMovieIsAlready_Disliked(curUser, movie))
            {
                RemoveDislikedMovie(curUser, movie);
                _notyf.Success("Successfuly removed Dislike from movie...");
            }

            if (CheckIfMovieIsAlready_Liked(curUser, movie))
            {
                RemoveLikedMovie(curUser, movie);
                _notyf.Success("Successfuly removed Like from movie...");
            }
            else
            {
                data.LikedMovies.Add(new LikedMovie
                {
                    MovieId = movie.Id,
                    UserId = curUser.Id
                });
                _notyf.Success("Successfuly marked movie as Liked...");

            }
            data.SaveChanges();
        }

        public void MarkMovieAsSeen(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.SeenMovies).FirstOrDefault(x => x.UserName == userName);
            CreateMovieIfItDoesNotExist(movieId);
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            if (CheckIfMovieIsAlready_Seen(curUser, movie))
            {
                RemoveSeenMovie(curUser, movie);
                _notyf.Success("Successfuly removed Seen from movie...");

            }
            else
            {
                data.SeenMovies.Add(new SeenMovie
                {
                    MovieId = movie.Id,
                    UserId = curUser.Id
                });
                _notyf.Success("Successfuly marked movie as Seen...");

            }
            data.SaveChanges();
        }
        private void CreateMovieIfItDoesNotExist(string moviePath)
        {
            var movieDetails = apiConnector.GetSpecificMovieDetails(moviePath).Result;
            if (data.Movies.FirstOrDefault(x => x.MoviePath == moviePath) == null)
            {
                var movieId = Guid.NewGuid().ToString();
                data.Movies.Add(new Movie
                {
                    Id = movieId,
                    MoviePath = moviePath,
                    Img = movieDetails.ImgUrl,
                    ReleaseDate = movieDetails.ReleaseDate,
                    Title = movieDetails.Title,
                });
                data.SaveChanges();
            }
        }

        private static bool CheckIfMovieIsAlready_Liked(User curUser, Movie movie)
        {
            return curUser.LikedMovies.FirstOrDefault(x => x.MovieId == movie.Id) != null ? true : false;
        }
        private static bool CheckIfMovieIsAlready_Disliked(User curUser, Movie movie)
        {
            return curUser.DislikedMovies.FirstOrDefault(x => x.MovieId == movie.Id) != null ? true : false;
        }
        private static bool CheckIfMovieIsAlready_Favourited(User curUser, Movie movie)
        {
            return curUser.FavouritedMovies.FirstOrDefault(x => x.MovieId == movie.Id) != null ? true : false;
        }
        private static bool CheckIfMovieIsAlready_Seen(User curUser, Movie movie)
        {
            return curUser.SeenMovies.FirstOrDefault(x => x.MovieId == movie.Id) != null ? true : false;
        }
        private static bool CheckIfMovieIsAlready_Flagged(User curUser, Movie movie)
        {
            return curUser.FlaggedMovies.FirstOrDefault(x => x.MovieId == movie.Id) != null ? true : false;
        }

        private void RemoveDislikedMovie(User curUser, Movie movie)
        {
            var dislikedMovie = curUser.DislikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (dislikedMovie != null)
            {
                data.DislikedMovies.Remove(dislikedMovie);
            }
        }
        private void RemoveLikedMovie(User curUser, Movie movie)
        {
            var likedMovie = curUser.LikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (likedMovie != null)
            {
                data.LikedMovies.Remove(likedMovie);
            }
        }
        private void RemoveFavouritedMovie(User curUser, Movie movie)
        {
            var favouritedMovie = curUser.FavouritedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (favouritedMovie != null)
            {
                data.FavouritedMovies.Remove(favouritedMovie);
            }
        }
        private void RemoveSeenMovie(User curUser, Movie movie)
        {
            var seenMovie = curUser.SeenMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (seenMovie != null)
            {
                data.SeenMovies.Remove(seenMovie);
            }
        }
        private void RemoveFlaggedMovie(User curUser, Movie movie)
        {
            var flaggedMovie = curUser.FlaggedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (flaggedMovie != null)
            {
                data.FlaggedMovies.Remove(flaggedMovie);
            }
        }
    }
}
