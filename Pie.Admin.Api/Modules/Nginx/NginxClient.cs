using System.Text.Json;

namespace Pie.Admin.Api.Modules.Nginx
{
    class NginxClient
    {
        private readonly HttpClient _client;

        NginxClient(HttpClient client)
        {
            _client = client;
        }

        internal async Task<JsonElement?> AddTenantAsync(string nginxBaseAddress, string tenantName, JsonElement reversePort)
        {
            _client.BaseAddress = new Uri($"http://{nginxBaseAddress}");

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"/nginx_domains/_doc/{tenantName}", reversePort);
                if (response.IsSuccessStatusCode)
                {
                    using Stream contentStream = await response.Content.ReadAsStreamAsync();
                    using JsonDocument document = await JsonDocument.ParseAsync(contentStream);
                    return document.RootElement.Clone();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }

            return null;
        }
    }
}
