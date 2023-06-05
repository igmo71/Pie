using Pie.Data.Models;

namespace Pie.Connectors.Connector1c.Models1c
{
    public class ProductDto1c
    {
        public string Ref_Key { get; set; } = null!;
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }

        public static Product MapToProduct(ProductDto1c dto)
        {
            Product product = new()
            {
                Id = Guid.Parse(dto.Ref_Key),
                Name = dto.Description,
                Active = !dto.DeletionMark
            };
            return product;
        }

        public static List<Product> MapToProductList(List<ProductDto1c> dtos)
        {
            List<Product> products = new();
            foreach(var dto in dtos)
            {
                products.Add(MapToProduct(dto));
            }
            return products;
        }
    }
}
