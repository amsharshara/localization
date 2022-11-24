using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication14.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPostAR()
        {
            return Redirect("~/?culture=ar-SY");
        }
        public IActionResult OnPostEN()
        {
            return Redirect("~/?culture=en-US");
        }
    }
}