using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Requests.BookRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.LibraryPages
{
    public class BookReadingModel : PageModel
    {
        private readonly IMediator mediator = default!;
        public BookReadingModel(IMediator mediator) : base() => this.mediator = mediator;

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = default!;
        public string PageContent { get; set; } = default!;
        public BookDto? BookInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var requestResult = await this.mediator.Send(new GetBookPageRequest(this.CurrentPage + 1, this.BookId));
            if (requestResult == null) return this.BadRequest("Страница не доступна");

            if((this.BookInfo = await this.mediator.Send(new GetBookRequest(this.BookId))) == null)
            {
                return this.BadRequest("Публикация не найдена");
            }
            this.PageContent = requestResult;
            return this.Page();
        }
    }
}
