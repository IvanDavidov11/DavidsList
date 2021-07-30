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
    public class GetInformationFromApi : IGetInformationFromApi
    {
        private static HttpClient client = new HttpClient();
        public GetInformationFromApi()
        {

        }

        private async Task<List<TopRatedMoviesApiModel>> GetIdsForTopRatedMoviesFromApi()
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

        private async Task<List<MovieQuickShowcaseViewModelWithRaiting>> GetMovieDetailsFromApi_Id_Raiting(List<TopRatedMoviesApiModel> idsOfMovies)
        {
            var result = new List<MovieQuickShowcaseViewModelWithRaiting>();

            foreach (var curMovie in idsOfMovies)
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-details?tconst={curMovie.id}"),
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
                    result.Add(new MovieQuickShowcaseViewModelWithRaiting
                    {
                        Title = bodyJson.title,
                        ImgUrl = bodyJson.image.url,
                        Year = bodyJson.year,
                        MoviePath = CleanUpMoviePath(bodyJson.id),
                        Raiting = curMovie.chartRating,
                    });
                }
            }
            return result;
        }

        public List<MovieQuickShowcaseViewModelWithRaiting> GetTopRatedMovieShowcaseViewModel()
        {
            List<TopRatedMoviesApiModel> idsOfMovies = GetIdsForTopRatedMoviesFromApi().Result;
            List<MovieQuickShowcaseViewModelWithRaiting> model = GetMovieDetailsFromApi_Id_Raiting(idsOfMovies).Result;
            return model;
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




        private async Task<List<string>> GetIdsForMostPopularMoviesFromApi()
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
        private async Task<List<MovieQuickShowcaseViewModel>> GetMovieDetailsFromApi_Id(List<string> idsOfMovies)
        {
            var result = new List<MovieQuickShowcaseViewModel>();

            foreach (var curId in idsOfMovies)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-details?tconst={curId}"),
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
                    result.Add(new MovieQuickShowcaseViewModel
                    {
                        Title = bodyJson.title,
                        ImgUrl = bodyJson.image.url,
                        Year = bodyJson.year,
                        MoviePath = CleanUpMoviePath(bodyJson.id),
                    });
                }
            }
            return result;
        }

        public List<MovieQuickShowcaseViewModel> GetMostPopularMovieShowcaseViewModel()
        {
            List<string> idsOfMovies = GetIdsForMostPopularMoviesFromApi().Result;
            List<MovieQuickShowcaseViewModel> model = GetMovieDetailsFromApi_Id(idsOfMovies).Result;
            return model;
        }


    }
}
