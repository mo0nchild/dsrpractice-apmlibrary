using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels.ComponentsViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace APMLibrary.Web.Pages.AdminPages
{
    [Authorize(Policy = "Admin")]
    public class PublishSettingsModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        private readonly ListCatalogOptions options = default!;
        public virtual ListCatalogOptions CatalogOptions { get => this.options; }

        public PublishSettingsModel(IMediator mediator, IOptions<ListCatalogOptions> options, IMapper mapper) : base()
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.options = options.Value;
        }

        [BindProperty(SupportsGet = true)]
        public bool? IsPublished { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public OrderType OrderType { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = default!;
        public BookCatalogViewModel Catalog { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var requestResult = await this.mediator.Send(new GetBooksListRequest()
            {
                IsPublished = this.IsPublished,
                SortingType = this.mapper.Map<SortingType>(this.OrderType),

                Skip = this.CurrentPage * this.CatalogOptions.MaxItemsOnPage,
                Take = this.CatalogOptions.MaxItemsOnPage,
            });
            this.Catalog = new BookCatalogViewModel()
            {
                AllCount = requestResult.AllBooksCount,
                Items = this.mapper.Map<IEnumerable<CatalogItemViewModel>>(requestResult.Books),
            };
            return this.Page();
        }
    }
}
