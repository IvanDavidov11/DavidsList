namespace DavidsList.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Newtonsoft.Json;
    using DavidsList.Models.API;
    using System.Threading.Tasks;
    using static Data.DataConstants;
    using System.Collections.Generic;
    using DavidsList.Models.ViewModels;
    using System.Threading;

    public class GetInformationFromApi : IGetInformationFromApi
    {
        private static HttpClient client = new HttpClient();
        public GetInformationFromApi()
        {

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
                    });
                }
        }
        public async Task<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>> GetMoviesInParallel_MostRated()
        {
            var idsOfMoviesWithRaiting = GetIdsForMoviesInApi_MostRated().Result;

            var movies = new List<MovieQuickShowcaseViewModelWithRaiting>();
            var batchSize = 5;
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
                });
            }
        }
        public async Task<IEnumerable<MovieQuickShowcaseViewModel>> GetMoviesInParallel_MostPopular()
        {
            List<string> idsOfMovies = GetIdsForMoviesInApi_MostPopular().Result;
            var users = new List<MovieQuickShowcaseViewModel>();
            var batchSize = 5;
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

                string longPlot = bodyJson.plotSummary != null ? bodyJson.plotSummary.text : "This movie has no summary of its plot...";

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
                    ShortPlot = bodyJson.plotOutline.text,
                    LongPlot = longPlot,
                };
            }
        }

        public string CleanUpMoviePath(string path)
        {
            return path.Substring(7, path.Length - 8);
        }
    }
}
