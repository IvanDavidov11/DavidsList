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
    using AspNetCoreHero.ToastNotification.Abstractions;
    using DavidsList.Models;
    using DavidsList.Data.DbModels;

    public class GetInformationFromApi : IGetInformationFromApi
    {
        private static HttpClient client = new HttpClient();
        private readonly DavidsListDbContext data;
        private readonly IUserService user;
        private Random rngPicker = new Random();
        private readonly INotyfService _notyf;
        public GetInformationFromApi(DavidsListDbContext db, IUserService userService, INotyfService notyf)
        {
            this.data = db;
            this.user = userService;
            _notyf = notyf;
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
                    Buttons = CreateButtonModel(movieId)
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
                    Buttons = CreateButtonModel(CleanUpMoviePath(bodyJson.id))
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
                    Buttons = CreateButtonModel(moviePath),
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
                                Buttons = CreateButtonModel(moviePath),
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
                .ThenInclude(x => x.Genre)
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
                .AsSplitQuery()
                .FirstOrDefault(x => x.UserName == uName);
            return curUser;
        }
        public string CleanUpMoviePath(string path)
        {
            return path.Substring(7, path.Length - 8);
        }

        public Button CreateButtonModel(string movieId)
        {
            var curUser = GetUserFromService();
            if (!this.user.GetUser().Identity.IsAuthenticated)
            {
                return null;
            }
            var result = new Button
            {
                IsSeen = curUser.SeenMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                IsLiked = curUser.LikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                IsDisliked = curUser.DislikedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                IsFavourited = curUser.FavouritedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
                IsFlagged = curUser.FlaggedMovies.FirstOrDefault(x => x.Movie.MoviePath == movieId) == null ? false : true,
            };
            return result;
        }

        public string GetRandomMoviePath_Surprise()
        {
            var pickedGenre = PickRandomGenreFrom_AllGenres();
            var allMoviesOfGenre = GetMoviesFromGenre(pickedGenre.ApiPath).Result;
            var pickedMovie = CleanUpMoviePath(PickRandomMovie(allMoviesOfGenre));
            _notyf.Custom($"Suggested movie in genre: {pickedGenre.GenreType}", null, "yellow");
            return pickedMovie;
        }
        public string GetRandomMoviePath_Preferred()
        {
            var pickedGenre = PickRandomGenreFrom_PreferredGenres();
            var allMoviesOfGenre = GetMoviesFromGenre(pickedGenre.ApiPath).Result;
            var pickedMovie = CleanUpMoviePath(PickRandomMovie(allMoviesOfGenre));
            _notyf.Custom($"Suggested movie in genre: {pickedGenre.GenreType}", null, "yellow");
            return pickedMovie;
        }
        public string GetRandomMoviePath_Specific(int genre)
        {
            var pickedGenre = PickRandomGenreFrom_Specific(genre);
            var allMoviesOfGenre = GetMoviesFromGenre(pickedGenre.ApiPath).Result;
            var pickedMovie = CleanUpMoviePath(PickRandomMovie(allMoviesOfGenre));
            _notyf.Custom($"Suggested movie in genre: {pickedGenre.GenreType}", null, "yellow");
            return pickedMovie;
        }



        private async Task<List<string>> GetMoviesFromGenre(string genre)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-popular-movies-by-genre?genre={genre}"),
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
                return bodyJson;
            }
        }
        private string PickRandomMovie(List<string> movies)
        {
            var result = movies[rngPicker.Next(0, movies.Count)];
            return result;
        }
        private Genre PickRandomGenreFrom_AllGenres()
        {
            var allGenres = data.Genres.ToList();
            return allGenres[this.rngPicker.Next(0, allGenres.Count)];
        }
        private Genre PickRandomGenreFrom_PreferredGenres()
        {
            var curUser = user.GetUser().Identity.Name;
            var preferredGenres = data.Users.Include(x => x.UserGenres).ThenInclude(x => x.Genre).FirstOrDefault(x => x.UserName == curUser).UserGenres;
            return preferredGenres.ElementAt(this.rngPicker.Next(0, preferredGenres.Count)).Genre;
        }
        private Genre PickRandomGenreFrom_Specific(int genre)
        {
            return data.Genres.First(x => x.Id == genre);
        }

        public bool CheckIfUserHasFavouritedGenre()
        {
            var username = user.GetUser().Identity.Name;
            var curUser = data.Users.Include(x => x.UserGenres).First(x => x.UserName == username);
            return curUser.UserGenres.Count <= 0 ? false : true;
        }
    }
}
