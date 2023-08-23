namespace Pie.Data.Models.Out
{
    public class DocOutBaseDocDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public static BaseDoc MapToBaseDoc(DocOutBaseDocDto dto)
        {
            BaseDoc baseDoc = new()
            {
                Id = dto.Id,
                Name = dto.Name
            };

            return baseDoc;
        }

        public static List<BaseDoc> MapToBaseDocList(List<DocOutBaseDocDto> dtoList)
        {
            List<BaseDoc> list = new();
            foreach (var dto in dtoList)
            {
                list.Add(MapToBaseDoc(dto));
            }

            return list;
        }

        public static DocOutBaseDoc MapToDocOutBaseDoc(Guid docId, DocOutBaseDocDto dto)
        {
            DocOutBaseDoc docOutBaseDoc = new()
            {
                DocId = docId,
                BaseDocId = dto.Id
            };
            return docOutBaseDoc;
        }

        public static List<DocOutBaseDoc> MapToDocOutBaseDocList(Guid docId, List<DocOutBaseDocDto> dtoList)
        {
            List<DocOutBaseDoc> list = new();

            foreach (var dto in dtoList)
                list.Add(MapToDocOutBaseDoc(docId, dto));

            return list;
        }

        public static DocOutBaseDocDto MapFromDocOutBaseDoc(DocOutBaseDoc item)
        {
            DocOutBaseDocDto docOutBaseDocDto = new()
            {
                Id = item.BaseDocId,
                Name = item.BaseDoc?.Name
            };
            return docOutBaseDocDto;
        }

        public static List<DocOutBaseDocDto> MapFromDocOutBaseDocList(List<DocOutBaseDoc> list)
        {
            List<DocOutBaseDocDto> dtoList = new();

            foreach(var item in list)
                dtoList.Add(MapFromDocOutBaseDoc(item));

            return dtoList;
        }
    }
}
