using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.LibraryPages
{
    public partial class LibraryCatalogModel : PageModel
    {
        public LibraryCatalogModel() : base() { }
        public async Task<IActionResult> OnGetAsync() => this.Page();
    }
}
