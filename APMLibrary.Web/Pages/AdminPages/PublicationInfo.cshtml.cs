using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.ViewModels.AdminViewModels;
using APMLibrary.Web.ViewModels.BookViewModels;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.AdminPages
{
    [Authorize(Policy = "Admin"), ValidateAntiForgeryToken]
    public class PublicationInfoModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        public PublicationInfoModel(IMapper mapper, IMediator mediator) : base()
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; } = default!;
        public BookInfoViewModel BookInfo { get; set; } = new();

        public IEnumerable<LanguageViewModel> Languages { get; set; } = default!;
        public IEnumerable<GenreViewModel> Genres { get; set; } = default!;
        public IEnumerable<BookTypeViewModel> BookTypes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var requestResult = await this.mediator.Send(new GetBookRequest(this.BookId));
            if (requestResult == null) return this.BadRequest("Публикация не найдена");
            try
            {
                this.Languages = this.mapper.Map<IEnumerable<LanguageViewModel>>(await this.mediator.Send(new GetLanguagesRequest()));
                this.BookTypes = this.mapper.Map<IEnumerable<BookTypeViewModel>>(await this.mediator.Send(new GetBookTypesRequest()));
                this.Genres = this.mapper.Map<IEnumerable<GenreViewModel>>(await this.mediator.Send(new GetGenresRequest()));
            }
            catch (Exception requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            this.BookInfo = this.mapper.Map<BookInfoViewModel>(requestResult);
            return this.Page();
        }

        public async Task<IActionResult> OnPostStateChangeAsync([FromForm] bool state)
        {
            try { await this.mediator.Send(new SetPublishedCommand() { BookId = this.BookId, IsPublished = state }); }
            catch (Exception requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/AdminPages/PublicationInfo", new { BookId = this.BookId });
        }

        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            try { await this.mediator.Send(new DeleteBookCommand(this.BookId)); }
            catch (Exception requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                this.BookId = id;
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/AdminPages/PublishSettings");
        }

        public async Task<IActionResult> OnPostAsync(EditBookViewModel viewModel)
        {
            if (!this.ModelState.IsValid) return await this.OnGetAsync();
            try
            {
                await this.mediator.Send(this.mapper.Map<UpdateBookCommand>(viewModel));
            }
            catch (NotFoundException requestError)
            {
                this.ModelState.AddModelError("", requestError.Message);
                return await this.OnGetAsync();
            }
            return this.RedirectToPage("/AdminPages/PublicationInfo", new { BookId = this.BookId });
        }
    }
}
