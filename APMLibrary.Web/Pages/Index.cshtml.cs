using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels.ComponentsViewModels;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace APMLibrary.Web.Pages
{
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        public readonly ListCatalogOptions pageOptions = default!;

        protected readonly IMapper mapper = default!;
        protected readonly IMediator mediator = default!;

        public IndexModel(IOptions<ListCatalogOptions> options, IMapper mapper, IMediator mediator) : base()
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.pageOptions = options.Value;
        }

        [FromQuery, BindProperty(SupportsGet = true)]
        public FilterViewModel FilterModel { get; set; } = new();

        [FromQuery, BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = default!;

        public BookCatalogViewModel Catalog { get; set; } = new();
        public IEnumerable<GenreViewModel> Genres { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            this.FilterModel.Language = FilterModel.Language == "null" ? null : FilterModel.Language;
            var requestResult = await this.mediator.Send(new GetBooksListRequest()
            {
                SortingType = this.mapper.Map<SortingType>(this.FilterModel.OrderType),
                IsPublished = true,
                GenreFilter = this.FilterModel.CategoryGenre,

                LanguageFilter = this.FilterModel.Language,
                TextFilter = this.FilterModel.SearchingText,

                Skip = this.pageOptions.MaxItemsOnPage * this.CurrentPage,
                Take = this.pageOptions.MaxItemsOnPage,
            });
            this.Catalog = new BookCatalogViewModel()
            {
                AllCount = requestResult.AllBooksCount,
                Items = this.mapper.Map<IEnumerable<CatalogItemViewModel>>(requestResult.Books),
            };
            this.Genres = this.mapper.Map<List<GenreViewModel>>(await this.mediator.Send(new GetGenresRequest()));
            return this.Page();
        }
    }
}