using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace docfx_plantuml
{
    public class PlantUMLConverter : IPlantUMLConverter
    {
        private static HttpClient _client = new HttpClient();
        private string pathTemplate = "plantuml/{0}/{1}";

        public PlantUMLConverter(string baseUri)
        {
            _client.BaseAddress = new Uri(baseUri);
        }

        public async Task<Stream> ConvertToImage(string plantUML, RenderFormat type)
        {
            var generatorUri = new Uri(_client.BaseAddress, String.Format(pathTemplate, type.ToString().ToLower(), plantUML.EncodePlantUML()));

            HttpResponseMessage response = await _client.GetAsync(generatorUri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            return null;
        }
    }
}
