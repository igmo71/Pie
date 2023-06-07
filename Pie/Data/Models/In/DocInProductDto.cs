namespace Pie.Data.Models.In
{
    public class DocInProductDto
    {
        public Guid ProductId { get; set; }
        public int LineNumber { get; set; }
        public float CountPlan { get; set; }
        public float CountFact { get; set; }
        public string? Unit { get; set; }
        public float Weight { get; set; }

        public static DocInProduct MapToDocInProduct(Guid docId, DocInProductDto dto)
        {
            DocInProduct item = new()
            {
                DocId = docId,
                ProductId = dto.ProductId,
                LineNumber = dto.LineNumber,
                CountPlan = dto.CountPlan,
                CountFact = dto.CountFact,
                Unit = dto.Unit,
                Weight = dto.Weight
            };
            return item;
        }

        public static List<DocInProduct> MapToDocInProductList(Guid docId, List<DocInProductDto> dtoList)
        {
            List<DocInProduct> list = new();

            foreach (DocInProductDto dto in dtoList)
                list.Add(MapToDocInProduct(docId, dto));

            return list;
        }

        public static DocInProductDto MapFromDocInProduct(DocInProduct item)
        {
            DocInProductDto dto = new()
            {
                ProductId = item.ProductId,
                LineNumber = item.LineNumber,
                CountPlan = item.CountPlan,
                CountFact = item.CountFact,
                Unit = item.Unit,
                Weight = item.Weight
            };

            return dto;
        }

        public static List<DocInProductDto> MapFromDocInProductList(List<DocInProduct> list)
        {
            List<DocInProductDto> dtoList = new();

            foreach (DocInProduct item in list)
                dtoList.Add(MapFromDocInProduct(item));

            return dtoList;
        }
    }
}
