using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.History.Pages.DocsIn
{
    public class IndexModel : PageModel
    {
        private readonly DocInHistoryService _docHistoryService;
        private readonly DocInProductHistoryService _docProductHistoryService;

        public IndexModel(DocInHistoryService docHistoryService, DocInProductHistoryService docProductHistoryService)
        {
            _docHistoryService = docHistoryService;
            _docProductHistoryService = docProductHistoryService;
        }

        public IList<DocInHistory> DocHistory { get; set; } = default!;
        public IList<DocInProductHistory> DocProductHistory { get; set; } = default!;
        public string? CurrentFilter { get; set; }
        public Guid? DocId { get; set; }

        public async Task OnGetAsync(string? searchString, Guid? docId)
        {
            CurrentFilter = searchString;
            DocId = docId;

            DocHistory = await _docHistoryService.GetListAsync(searchString, docId);

            if (!string.IsNullOrEmpty(searchString) || docId != null)
                DocProductHistory = await _docProductHistoryService.GetListAsync(searchString, docId);
        }
    }
}
