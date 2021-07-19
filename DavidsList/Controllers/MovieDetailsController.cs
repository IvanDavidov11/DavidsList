namespace DavidsList.Controllers
{
    using DavidsList.Models.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class MovieDetailsController : Controller
    {
        public IActionResult Index(string id)
        {
            var model =  GetMovieDetailsFromApi(id).Result;
            return View(model);
        }

        private static async Task<MovieDetailsViewModel> GetMovieDetailsFromApi(string moviePath)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-overview-details?tconst={moviePath}&currentCountry=US"),
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
                var bodyJson = JsonConvert.DeserializeObject<MovieDetailsApiModel>(body);
                return new MovieDetailsViewModel
                {
                    Title = bodyJson.title.title,
                    ImgUrl= bodyJson.title.image.url,
                    MoviePath = moviePath,
                    RunningTimeInMinutes = bodyJson.title.runningTimeInMinutes,
                    ReleaseDate = bodyJson.releaseDate,
                    Raiting = bodyJson.ratings.rating,
                    RaitingCount = bodyJson.ratings.ratingCount,
                    Genres = bodyJson.genres,
                    ShortPlot = bodyJson.plotOutline.text,
                    LongPlot = bodyJson.plotSummary.text,
                };
            }
        }
    }
}
