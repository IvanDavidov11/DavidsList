namespace DavidsList.Controllers
{
    using DavidsList.Models.API;
    using DavidsList.Models.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using static Data.DataConstants;

    public class TopRatedMoviesController : Controller
    {
        private static HttpClient client = new HttpClient();


        public IActionResult Index()
        {


            List<TopRatedMoviesApiModel> idsOfMovies = GetIdsForMostPopularMoviesFromApi().Result;
            List<MovieQuickShowcaseViewModelWithRaiting> model = GetMovieDetailsFromApiWithId(idsOfMovies).Result;

            return View(model);
        }

        private async static Task<List<MovieQuickShowcaseViewModelWithRaiting>> GetMovieDetailsFromApiWithId(List<TopRatedMoviesApiModel> idsOfMovies)
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
                        { "x-rapidapi-key", "6e96b86691mshebfd322284464b6p1c6f16jsnc9e27395f9df" },
                        { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
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

        public async static Task<List<TopRatedMoviesApiModel>> GetIdsForMostPopularMoviesFromApi()
        {

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/get-top-rated-movies"),
                Headers =
                 {
                     { "x-rapidapi-key", "6e96b86691mshebfd322284464b6p1c6f16jsnc9e27395f9df" },
                     { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
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

        public static string CleanUpMoviePath(string path)
        {
            return path.Substring(7, path.Length - 8);
        }
    }
}
