namespace DavidsList.Infrastructures
{
    using DavidsList.Data;
    using DavidsList.Models.API;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using static Data.DataConstants; 
    public static class AppBuilderExtenstions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var data = scopedService.ServiceProvider.GetService<DavidsListDbContext>();

            data.Database.Migrate();

            if (!data.Genres.Any())
            {
                data.Genres.AddRange(SeedDbWithAllGenres(data).Result);
                data.SaveChanges();
            }

            return app;
        }

        public static async Task<List<Data.DbModels.Genre>> SeedDbWithAllGenres (DavidsListDbContext context)
        {
            var client = new HttpClient();

            var result = new List<Data.DbModels.Genre>();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/list-popular-genres"),
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
                var bodyJson = JsonConvert.DeserializeObject<GenresFromApiModel>(body);

                foreach (var curGenre in bodyJson.genres)
                {
                    result.Add(new Data.DbModels.Genre
                    {
                        GenreType = curGenre.description,
                        ApiPath = curGenre.endpoint,
                    });
                }

                return result;
            }



        }
    }
}
