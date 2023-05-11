using Pie.Data.Models.In;
using Pie.Data.Models.Out;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class Service1c
    {
        private readonly HttpClient _client1cUt;
        private readonly JsonSerializerOptions _serializerOptions;

        public Service1c(IHttpClientFactory clientFactory)
        {
            _client1cUt = clientFactory.CreateClient(nameof(Client1c));
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<Service1cResult> SendInAsync(DocIn doc)
        {
            Service1cResult result = new();

            return result;
        }

        public async Task<Service1cResult> SendOutAsync(DocOut doc)
        {
            Service1cResult result = new();

            return result;
        }
    }
}
