using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class IndexModel : PageModel
    {
        private readonly DocOutHistoryService _docHistoryService;
        private readonly DocOutProductHistoryService _docProductHistoryService;

        public IndexModel(DocOutHistoryService docHistoryService, DocOutProductHistoryService docProductHistoryService)
        {
            _docHistoryService = docHistoryService;
            _docProductHistoryService = docProductHistoryService;
        }

        public IList<DocOutHistory> DocOutHistory { get; set; } = default!;
        public IList<DocOutProductHistory> DocOutProductHistory { get; set; } = default!;
        public string? CurrentFilter { get; set; }
        public Guid? DocId { get; set; }

        public async Task OnGetAsync(string? searchString, Guid? docId)
        {
            CurrentFilter = searchString;
            DocId = docId;

            DocOutHistory = await _docHistoryService.GetListAsync(searchString, docId);
            if (!string.IsNullOrEmpty(searchString) || docId != null)
                DocOutProductHistory = await _docProductHistoryService.GetListAsync(searchString, docId);            
        }
    }
}
