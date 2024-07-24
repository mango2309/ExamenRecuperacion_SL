using ExamenRecuperacion_SL.ModelsSL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenRecuperacion_SL.ServicesSL
{
    public class RazaApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<RazaPerroSL>> GetBreedsAsync()
        {
            var response = await client.GetStringAsync("https://dog.ceo/api/breeds/list/all");
            var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<string>>>>(response);

            var breeds = new List<RazaPerroSL>();
            foreach (var breed in data["message"])
            {
                var images = await GetBreedImagesAsync(breed.Key);
                breeds.Add(new RazaPerroSL { Nombre = breed.Key, SubBreeds = breed.Value, Images = images });
            }
            return breeds;
        }

        public async Task<List<string>> GetBreedImagesAsync(string breed)
        {
            var response = await client.GetStringAsync($"https://dog.ceo/api/breed/{breed}/images/random/3");
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(response);
            return data["message"];
        }
    }
}
