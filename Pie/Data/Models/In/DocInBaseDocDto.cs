namespace Pie.Data.Models.In
{
    public class DocInBaseDocDto
    {
        public Guid BaseDocId { get; set; }
        public string? Name { get; set; }

        public static BaseDoc MapToBaseDoc(DocInBaseDocDto dto)
        {
            BaseDoc baseDoc = new()
            {
                Id = dto.BaseDocId,
                Name = dto.Name
            };

            return baseDoc;
        }

        public static List<BaseDoc> MapToBaseDocList(List<DocInBaseDocDto> dtos)
        {
            List<BaseDoc> list = new();
            foreach (var dto in dtos)
            {
                list.Add(MapToBaseDoc(dto));
            }

            return list;
        }

        public static DocInBaseDoc MapToDocInBaseDoc(Guid docId, DocInBaseDocDto dto)
        {
            DocInBaseDoc docInBaseDoc = new()
            {
                DocId = docId,
                BaseDocId = dto.BaseDocId
            };
            return docInBaseDoc;
        }

        public static List<DocInBaseDoc> MapToDocInBaseDocList(Guid docId, List<DocInBaseDocDto> dtoList)
        {
            List<DocInBaseDoc> list = new();

            foreach (var dto in dtoList)
                list.Add(MapToDocInBaseDoc(docId, dto));

            return list;
        }

        public static DocInBaseDocDto MapFromDocInBaseDoc(DocInBaseDoc item)
        {
            DocInBaseDocDto docInBaseDocDto = new()
            {
                BaseDocId = item.BaseDocId,
                Name = item.BaseDoc?.Name
            };
            return docInBaseDocDto;
        }

        public static List<DocInBaseDocDto> MapFromDocInBaseDocList(List<DocInBaseDoc> list)
        {
            List<DocInBaseDocDto> dtoList = new();

            foreach (var item in list)
                dtoList.Add(MapFromDocInBaseDoc(item));

            return dtoList;
        }
    }
}
