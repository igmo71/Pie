using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.QueuesIn
{
    public class CreateModel : PageModel
    {
        private readonly QueueInService _queueService;

        public CreateModel(QueueInService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public QueueIn Queue { get; set; } = default!;

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
