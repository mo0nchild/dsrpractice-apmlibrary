using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Dal;
using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels.BookViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APMLibrary.Web.Pages.AdminPages
{
    [Authorize(Policy = "Admin")]
    public class RatingsSettingsModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;
        private readonly ListCatalogOptions options = default!;

        public virtual ListCatalogOptions PageOptions { get => this.options; }
        public RatingsSettingsModel(IMediator mediator, IMapper mapper, IOptions<ListCatalogOptions> options) : base()
        {
            this.options = options.Value;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int BookId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = default!;

        public IEnumerable<RatingViewModel> Ratings { get; set; } = default!;
        public int AllCount { get; set; } = default!;
        public string Title { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var requestResult = await this.mediator.Send(new GetRatingsRequest()
                {
                    BookId = this.BookId,
                    Skip = this.options.MaxItemsOnPage * this.CurrentPage,
                    Take = this.options.MaxItemsOnPage,
                });
                this.Ratings = this.mapper.Map<IEnumerable<RatingViewModel>>(requestResult.Ratings);
                this.AllCount = requestResult.AllCount;

                var publish = await this.mediator.Send(new GetBookRequest(this.BookId));
                if (publish == null) throw new NotFoundException("Публикация не найдена", this.BookId);

                this.Title = publish.Title;
            }
            catch (NotFoundException requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }
        public async Task<IActionResult> OnGetDeleteAsync(int ratingId)
        {
            try { await this.mediator.Send(new DeleteRatingCommand(ratingId)); }
            catch(Exception requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.RedirectToPage("/AdminPages/RatingsSettings", new { BookId = this.BookId });
        }
    }
}
