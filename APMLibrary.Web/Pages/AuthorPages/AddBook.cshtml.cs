using APMLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.AuthorPages
{
    [ValidateAntiForgeryToken]
    public partial class AddBookModel : PageModel
    {
        public AddBookModel() : base() { }

        public async Task<IActionResult> OnGetAsync() => this.Page();


        [FromForm, BindProperty(Name = "BookModel")]
        public CreateBookViewModel BookModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }
            return this.RedirectToPage("/Index");
        }
    }
}
