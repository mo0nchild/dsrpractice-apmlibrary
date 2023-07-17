using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Bll.Requests.PublisherRequests;
using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.Middlewares;
using APMLibrary.Web.ViewModels.BookViewModels;
using APMLibrary.Web.ViewModels.PublisherViewModels;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.PublisherPages
{
    [Authorize(Policy = "User"), TypeFilter(typeof(PublisherFilter)), ValidateAntiForgeryToken]
    public class EditBookModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        public int ProfileId { get => int.Parse(this.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid)); }
        public EditBookModel(IMediator mediator, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; } = default!;

        [FromForm, BindProperty]
        public UpdateBookViewModel Publication { get; set; } = default!;
        public IEnumerable<LanguageViewModel> Languages { get; set; } = default!;
        public IEnumerable<GenreViewModel> Genres { get; set; } = default!;
        public IEnumerable<BookTypeViewModel> BookTypes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                this.Languages = this.mapper.Map<IEnumerable<LanguageViewModel>>(await this.mediator.Send(new GetLanguagesRequest()));
                this.BookTypes = this.mapper.Map<IEnumerable<BookTypeViewModel>>(await this.mediator.Send(new GetBookTypesRequest()));
                this.Genres = this.mapper.Map<IEnumerable<GenreViewModel>>(await this.mediator.Send(new GetGenresRequest()));

                this.Publication = this.mapper.Map<UpdateBookViewModel>(await this.mediator.Send(new GetBookRequest(this.BookId)));
            }
            catch (Exception requestError) 
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }
        public async Task<IActionResult> OnGetDeleteAsync()
        {
            try
            {
                await this.mediator.Send(new DeleteBookCommand(this.BookId));
            }
            catch(Exception requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/PublisherPages/PublisherInfo");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            this.BookId = this.Publication.BookId;
            if (!this.ModelState.IsValid) return await this.OnGetAsync();
            try
            {
                await this.mediator.Send(this.mapper.Map<UpdateBookCommand>(this.Publication));
            }
            catch(NotFoundException requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/LibraryPages/BookInfo", new { BookId = this.BookId });
        }
    }
}
