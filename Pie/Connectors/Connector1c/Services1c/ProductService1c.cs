using Pie.Connectors.Connector1c.Models1c;
using Pie.Data.Models;
using System.Text.Json;

namespace Pie.Connectors.Connector1c.Services1c
{
    public class ProductService1c
    {
        private readonly string SUBJECT = "Catalog_Номенклатура";
        private readonly string SELECT = "Ref_Key,Code,Description,DeletionMark";
        private readonly string FILTER = "IsFolder eq false and DeletionMark eq false";
        private readonly string ORDERBY = "Ref_Key";

        private readonly ODataService1c _oDataService1c;
        private readonly ILogger<ProductService1c> _logger;

        public ProductService1c(
            ODataService1c oDataService1c,
            ILogger<ProductService1c> logger)
        {

            _oDataService1c = oDataService1c;
            _logger = logger;
        }

        public async Task<int> GetCountAsync()
        {
            string uri = $"{SUBJECT}/$count?$filter={FILTER}";

            string response =  await _oDataService1c.GetAsync(uri);

            int productsCount = int.Parse(response);

            return productsCount;
        }

        public async Task<List<Product>?> GetListAsync(int top, int skip)
        {
            string uri = $"{SUBJECT}?$format=json&$select={SELECT}&$filter={FILTER}&$orderby={ORDERBY}&$top={top}&$skip={skip}";
            
            var response = await _oDataService1c.GetAsync(uri);

            RootObject<ProductDto1c>? rootObject = JsonSerializer.Deserialize<RootObject<ProductDto1c>>(response);            
           
            if (rootObject?.Value == null) return default;

            List<Product> result = ProductDto1c.MapToProductList(rootObject.Value);

            return result;
        }
    }
}
