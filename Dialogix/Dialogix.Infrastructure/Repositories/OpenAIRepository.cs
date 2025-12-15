using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public class OpenAIRepository : IOpenAIRepository
    {
        private readonly HttpClient _http;

        public OpenAIRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GenerarRespuestaAsync(string prompt)
        {
            var request = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new { role = "user", content = prompt.Trim() }
                } 
            };

            var response = await _http.PostAsJsonAsync("chat/completions", request);
            var respuesta = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            var texto = json
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return texto!;
        }
    }
}
