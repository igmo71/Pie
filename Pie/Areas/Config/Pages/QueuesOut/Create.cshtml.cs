using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class CreateModel : PageModel
    {
        private readonly QueueOutService _queueService;

        public CreateModel(QueueOutService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public QueueOut Queue { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _ = await _queueService.CreateAsync(Queue);

            return RedirectToPage("./Index");
        }
    }
}
