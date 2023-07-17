using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Bll.Requests.SupportRequests.Handlers;
using APMLibrary.Web.Middlewares;
using APMLibrary.Web.ViewModels.PublisherViewModels;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.AuthorPages
{
    [ValidateAntiForgeryToken, ModelStateFilter]
    [Authorize(Policy = "User")]
    public partial class AddBookModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public AddBookModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public List<LanguageViewModel> Languages { get; set; } = default!;
        public List<GenreViewModel> Genres { get; set; } = default!;
        public List<BookTypeViewModel> BookTypes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            this.Languages = this._mapper.Map<List<LanguageViewModel>>(await this._mediator.Send(new GetLanguagesRequest()));
            this.Genres = this._mapper.Map<List<GenreViewModel>>(await this._mediator.Send(new GetGenresRequest()));

            this.BookTypes = this._mapper.Map<List<BookTypeViewModel>>(await this._mediator.Send(new GetBookTypesRequest()));
            return this.Page();
        }
        public int PublisherId { get => int.Parse(this.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid)); }

        [FromForm, BindProperty(Name = "BookModel")]
        public CreateBookViewModel BookModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid) return await this.OnGetAsync();

            try { await this._mediator.Send(this._mapper.Map<CreateBookCommand>(this.BookModel)); }
            catch (CreateBookException requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/PublisherPages/PublisherInfo");
        }
    }
}
