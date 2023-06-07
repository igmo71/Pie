namespace Pie.Data.Models.Out
{
    public class DocOutProductDto
    {
        public Guid ProductId { get; set; }
        public int LineNumber { get; set; }
        public float CountPlan { get; set; }
        public float CountFact { get; set; }
        public string? Unit { get; set; }
        public float Weight { get; set; }

        public static DocOutProduct MapToDocOutProduct(Guid docId, DocOutProductDto dto)
        {
            DocOutProduct item = new()
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

        public static List<DocOutProduct> MapToDocOutProductList(Guid docId, List<DocOutProductDto> dtoList)
        {
            List<DocOutProduct> list = new();

            foreach (DocOutProductDto dto in dtoList)
                list.Add(MapToDocOutProduct(docId, dto));

            return list;
        }

        public static DocOutProductDto MapFromDocOutProduct(DocOutProduct item)
        {
            DocOutProductDto dto = new()
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

        public static List<DocOutProductDto> MapFromDocOutProductList(List<DocOutProduct> list)
        {
            List<DocOutProductDto> dtoList = new();

            foreach (DocOutProduct item in list)
                dtoList.Add(MapFromDocOutProduct(item));

            return dtoList;
        }
    }
}
