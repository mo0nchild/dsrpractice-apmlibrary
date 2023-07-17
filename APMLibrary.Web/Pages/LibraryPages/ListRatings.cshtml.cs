using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels.BookViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace APMLibrary.Web.Pages.LibraryPages
{
    [Authorize(Policy = "User")]
    public partial class ListRatingsModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;
        private readonly ListCatalogOptions options = default!;

        public virtual ListCatalogOptions PageOptions { get => this.options; }

        public ListRatingsModel(IMediator mediator, IMapper mapper, IOptions<ListCatalogOptions> options) : base()
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
                if (publish == null) throw new NotFoundException("���������� �� �������", this.BookId);

                this.Title = publish.Title;
            }
            catch(NotFoundException requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }
    }
}
