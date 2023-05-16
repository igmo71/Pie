using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class IndexModel : PageModel
    {
        private readonly DocOutProductHistoryService _docProductHistoryService;

        public IndexModel(DocOutProductHistoryService docProductHistoryService)
        {
            _docProductHistoryService = docProductHistoryService;
        }

        public IList<DocOutProductHistory> DocOutProductHistory { get; set; } = default!;
        public string? CurrentFilter { get; set; }
        public Guid? DocId { get; set; }

        public async Task OnGetAsync(string? searchString, Guid? docId)
        {

            CurrentFilter = searchString;
            DocId = docId;

            DocOutProductHistory = await _docProductHistoryService.GetListAsync(searchString, docId);
        }
    }
}
