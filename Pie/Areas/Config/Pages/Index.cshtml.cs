using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pie.Areas.Config.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
