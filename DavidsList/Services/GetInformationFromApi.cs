namespace DavidsList.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using static Data.DataConstants;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using DavidsList.Models.API.SearchResults;
    using DavidsList.Models.API.TopRatedMovies;
    using DavidsList.Models.API.MostPopularMovies;
    using DavidsList.Models.MovieDetails;
    using Microsoft.EntityFrameworkCore;
    using DavidsList.Data;

    public class GetInformationFromApi : IGetInformationFromApi
    {
        private static HttpClient client = new HttpClient();
        private readonly DavidsListDbContext data;
        private readonly IUserService user;

        public GetInformationFromApi(DavidsListDbContext db, IUserService userService)
        {
            this.data = db;
            this.user = userService;
        }

        private async Task<List<TopRatedMoviesApiModel>> GetIdsForMoviesInApi_MostRated()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/get-top-rated-movies"),
                Headers =
                        {
                            { "x-rapidapi-key", IMDbApiKey },
                            { "x-rapidapi-host", IMDbApiHost },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var bodyJson = JsonConvert.DeserializeObject<List<TopRatedMoviesApiModel>>(body);
                bodyJson.RemoveRange(AmountOfMoviesToPull, bodyJson.Count - AmountOfMoviesToPull);
                bodyJson = bodyJson.Select(x => { x.id = CleanUpMoviePath(x.id); return x; }).ToList();
                return bodyJson;
            }
        }
        private async Task<MovieQuickShowcaseViewModelWithRaiting> GetMovie_MostRated(string movieId, double raiting)
        {
            var curUser = GetUserFromService();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-details?tconst={movieId}"),
                Headers =
                        {
                            { "x-rapidapi-key", IMDbApiKey },
                            { "x-rapidapi-host", IMDbApiHost },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var bodyJson = JsonConvert.DeserializeObject<MovieQuickShowcaseJsonModel>(body);
                return (new MovieQuickShowcaseViewModelWithRaiting
                {
                    Title = bodyJson.title,
                    ImgUrl = bodyJson.image.url,
                    Year = bodyJson.year,
                    MoviePath = CleanUpMoviePath(bodyJson.id),
                    Raiting = raiting,
                    IsSeen = curUser.SeenMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsLiked = curUser.LikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsDisliked = curUser.DislikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsFavourited = curUser.FavouritedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsFlagged = curUser.FlaggedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,

                });
            }
        }
        public async Task<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>> GetMoviesInParallel_MostRated()
        {
            var idsOfMoviesWithRaiting = GetIdsForMoviesInApi_MostRated().Result;

            var movies = new List<MovieQuickShowcaseViewModelWithRaiting>();

            var batchSize = AmountOfMoviesToPull == 1 ? 1 : 5;

            int numberOfBatches = AmountOfMoviesToPull / batchSize;

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = idsOfMoviesWithRaiting.Skip(i * batchSize).Take(batchSize);

                var tasks = currentIds.Select(x => GetMovie_MostRated(x.id, x.chartRating));
                movies.AddRange(await Task.WhenAll(tasks));
                Thread.Sleep(1000);
            }

            return movies;
        }


        private async Task<List<string>> GetIdsForMoviesInApi_MostPopular()
        {

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/get-most-popular-movies?homeCountry=US&purchaseCountry=US&currentCountry=US"),
                Headers =
                 {
                     { "x-rapidapi-key", IMDbApiKey },
                     { "x-rapidapi-host", IMDbApiHost },
                 },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var bodyJson = JsonConvert.DeserializeObject<List<string>>(body);
                bodyJson.RemoveRange(AmountOfMoviesToPull, bodyJson.Count - AmountOfMoviesToPull);
                bodyJson = bodyJson.Select(x => { x = CleanUpMoviePath(x); return x; }).ToList();
                return bodyJson;
            }
        }
        private async Task<MovieQuickShowcaseViewModel> GetMovie_MostPopular(string movieId)
        {
            var curUser = GetUserFromService();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-details?tconst={movieId}"),
                Headers =
                 {
                     { "x-rapidapi-key", IMDbApiKey },
                     { "x-rapidapi-host", IMDbApiHost },
                 },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var bodyJson = JsonConvert.DeserializeObject<MovieQuickShowcaseJsonModel>(body);
                return (new MovieQuickShowcaseViewModel
                {
                    Title = bodyJson.title,
                    ImgUrl = bodyJson.image.url,
                    Year = bodyJson.year,
                    MoviePath = CleanUpMoviePath(bodyJson.id),
                    IsSeen = curUser.SeenMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsLiked = curUser.LikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsDisliked = curUser.DislikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsFavourited = curUser.FavouritedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                    IsFlagged = curUser.FlaggedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                });
            }
        }
        public async Task<IEnumerable<MovieQuickShowcaseViewModel>> GetMoviesInParallel_MostPopular()
        {
            List<string> idsOfMovies = GetIdsForMoviesInApi_MostPopular().Result;
            var users = new List<MovieQuickShowcaseViewModel>();
            var batchSize = AmountOfMoviesToPull == 1 ? 1 : 5;
            int numberOfBatches = AmountOfMoviesToPull / batchSize;

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = idsOfMovies.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(id => GetMovie_MostPopular(id));
                users.AddRange(await Task.WhenAll(tasks));
                Thread.Sleep(1000);
            }

            return users;
        }

        public async Task<MovieDetailsViewModel> GetSpecificMovieDetails(string moviePath)
        {
            var curUser = GetUserFromService();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-overview-details?tconst={moviePath}&currentCountry=US"),
                Headers =
                 {
                     { "x-rapidapi-key", IMDbApiKey },
                     { "x-rapidapi-host", IMDbApiHost },
                 },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var bodyJson = JsonConvert.DeserializeObject<MovieDetailsApiModel>(body);


                return new MovieDetailsViewModel
                {
                    Title = bodyJson.title.title,
                    ImgUrl = bodyJson.title.image.url,
                    MoviePath = moviePath,
                    RunningTimeInMinutes = bodyJson.title.runningTimeInMinutes,
                    ReleaseDate = bodyJson.releaseDate,
                    Raiting = bodyJson.ratings.rating,
                    RaitingCount = bodyJson.ratings.ratingCount,
                    Genres = bodyJson.genres,
                    ShortPlot = bodyJson.plotOutline != null ? bodyJson.plotOutline.text : "This movie has no plot outline...",
                    LongPlot = bodyJson.plotSummary != null ? bodyJson.plotSummary.text : "This movie has no summary of its plot...",
                    IsSeen = curUser.SeenMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                    IsLiked = curUser.LikedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                    IsDisliked = curUser.DislikedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                    IsFavourited = curUser.FavouritedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                    IsFlagged = curUser.FlaggedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                };
            }
        }


        public async Task<List<SearchResultsViewModel>> GetSearchResultModel(string query)
        {
            var curUser = GetUserFromService();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/find?q={query}"),
                Headers =
                 {
                     { "x-rapidapi-key", IMDbApiKey },
                     { "x-rapidapi-host", IMDbApiHost },
                 },
            };
            using (var response = await client.SendAsync(request))
            {
                var model = new List<SearchResultsViewModel>();
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var bodyJson = JsonConvert.DeserializeObject<SearchResultsApiModel>(body).results;
                foreach (var curMovieResult in bodyJson)
                {
                    try
                    {
                        if (!curMovieResult.id.Contains("/name"))
                        {
                            var moviePath = CleanUpMoviePath(curMovieResult.id);
                            model.Add(new SearchResultsViewModel
                            {
                                ImgUrl = curMovieResult.image.url,
                                MoviePath = moviePath,
                                Title = curMovieResult.title,
                                Year = curMovieResult.year,
                                IsSeen = curUser.SeenMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                                IsLiked = curUser.LikedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                                IsDisliked = curUser.DislikedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                                IsFavourited = curUser.FavouritedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                                IsFlagged = curUser.FlaggedMovies.FirstOrDefault(x => x.Movie.MoviePath == moviePath) == null ? false : true,
                            });
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return model;
            }
        }

        private Data.DbModels.User GetUserFromService()
        {
            var uName = user.GetUser().Identity.Name;
            var curUser = data.Users
                .Include(x => x.UserGenres)
                .ThenInclude(x=>x.Genre)
                .Include(x => x.FavouritedMovies)
                .ThenInclude(x => x.Movie)
                .Include(x => x.FlaggedMovies)
                .ThenInclude(x => x.Movie)
                .Include(x => x.LikedMovies)
                .ThenInclude(x => x.Movie)
                .Include(x => x.SeenMovies)
                .ThenInclude(x => x.Movie)
                .Include(x => x.DislikedMovies)
                .ThenInclude(x => x.Movie)
                .FirstOrDefault(x => x.UserName == uName);
            return curUser;
        }

        public string CleanUpMoviePath(string path)
        {
            return path.Substring(7, path.Length - 8);
        }
    }
}
