using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.ProfilePages
{
    [ValidateAntiForgeryToken, Authorize]
    public class ProfileInfoModel : PageModel
    {
        public ProfileInfoModel() : base() { }

        public async Task<IActionResult> OnGetAsync()
        {
            return this.Page();
        }
    }
}
