namespace DavidsList.Services
{
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Data.DbModels.ManyToManyTables;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;


    public class MarkMovieService : IMarkMovieService
    {
        private readonly DavidsListDbContext data;

        public MarkMovieService(DavidsListDbContext db)
        {
            this.data = db;
        }

        public void MarkMovieAsDisliked(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.DislikedMovies).Include(x => x.LikedMovies).FirstOrDefault(x => x.UserName == userName);
            if (data.Movies.FirstOrDefault(x => x.MoviePath == movieId) == null)
            {
                CreateMovie(movieId);
            }
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);
            CheckIfMovieIsAlreadyLiked_ForDisliked(curUser, movie);
            var movieIsDisliked = CheckIfMovieIsAlreadyDisliked_ForDisliked(curUser,movie);
            if (movieIsDisliked)
            {
                return;
            }

            data.DislikedMovies.Add(new DislikedMovie
            {
                MovieId = movie.Id,
                UserId = curUser.Id
            });
            data.SaveChanges();
        }

        public void MarkMovieAsFavourite(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.FavouritedMovies).FirstOrDefault(x => x.UserName == userName);
            if (data.Movies.FirstOrDefault(x => x.MoviePath == movieId) == null)
            {
                CreateMovie(movieId);
            }
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);
            

            data.FavouritedMovies.Add(new FavouritedMovie
            {
                MovieId = movie.Id,
                UserId = curUser.Id
            });
            data.SaveChanges();
        }

        public void MarkMovieAsFlagged(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.FlaggedMovies).FirstOrDefault(x => x.UserName == userName);
            if (data.Movies.FirstOrDefault(x => x.MoviePath == movieId) == null)
            {
                CreateMovie(movieId);
            }
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            data.FlaggedMovies.Add(new FlaggedMovie
            {
                MovieId = movie.Id,
                UserId = curUser.Id
            });
            data.SaveChanges();
        }

        public void MarkMovieAsLiked(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.LikedMovies).Include(x => x.DislikedMovies).FirstOrDefault(x => x.UserName == userName);
            if (data.Movies.FirstOrDefault(x => x.MoviePath == movieId) == null)
            {
                CreateMovie(movieId);
            }
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);
            CheckIfMovieIsAlreadyDisliked_ForLike(curUser, movie);
            var movieIsAlreadyLiked = CheckIfMovieIsAlreadyLiked_ForLike(curUser, movie);

            if (movieIsAlreadyLiked)
            {
                return;
            }

            data.LikedMovies.Add(new LikedMovie
            {
                MovieId = movie.Id,
                UserId = curUser.Id
            });
            data.SaveChanges();
        }

        private static bool CheckIfMovieIsAlreadyLiked_ForLike(User curUser, Movie movie)
        {
            var likedMovie = curUser.LikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (likedMovie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckIfMovieIsAlreadyDisliked_ForLike(User curUser, Movie movie)
        {
            var dislikedMovie = curUser.DislikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (dislikedMovie != null)
            {
                data.DislikedMovies.Remove(dislikedMovie);
            }
        }
        private void CheckIfMovieIsAlreadyLiked_ForDisliked(User curUser, Movie movie)
        {
            var likedMovie = curUser.LikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (likedMovie != null)
            {
                data.LikedMovies.Remove(likedMovie);
            }
        }


        private static bool CheckIfMovieIsAlreadyDisliked_ForDisliked(User curUser, Movie movie)
        {
            var dislikedMovie = curUser.DislikedMovies.FirstOrDefault(x => x.MovieId == movie.Id);
            if (dislikedMovie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void MarkMovieAsSeen(string movieId, string userName)
        {
            var curUser = data.Users.Include(x => x.SeenMovies).FirstOrDefault(x => x.UserName == userName);
            if (data.Movies.FirstOrDefault(x => x.MoviePath == movieId) == null)
            {
                CreateMovie(movieId);
            }
            var movie = data.Movies.FirstOrDefault(x => x.MoviePath == movieId);

            data.SeenMovies.Add(new SeenMovie
            {
                MovieId = movie.Id,
                UserId = curUser.Id
            });
            data.SaveChanges();
        }

        private void CreateMovie(string moviePath)
        {
            var movieId = Guid.NewGuid().ToString();
            data.Movies.Add(new Movie
            {
                Id = movieId,
                MoviePath = moviePath
            });
            data.SaveChanges();
        }
    }
}
