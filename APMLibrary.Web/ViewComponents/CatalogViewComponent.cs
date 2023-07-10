using APMLibrary.Bll.Requests.BookRequests;
using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels.ComponentsViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;

namespace APMLibrary.Web.ViewComponents
{
    public partial class CatalogComponentParameter : object
    {
        public string Title { get; set; } = default!;
        public string RedirectLink { get; set; } = default!;
        public GetBooksListRequest Request { get; set; } = default!;
    }

    [ViewComponent]
    public partial class CatalogViewComponent : ViewComponent
    {
        protected readonly IMediator mediator = default!;
        protected readonly IMapper mapper = default!;
        public CatalogViewComponent(IMapper mapper, IMediator mediator) : base() 
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public virtual async Task<IViewComponentResult> InvokeAsync(CatalogComponentParameter param)
        {
            this.ViewBag.RedirectLink = param.RedirectLink;
            var requestResult = await this.mediator.Send(param.Request);

            return this.View(new BookCatalogViewModel()
            {
                AllCount = requestResult.AllBooksCount,
                Name = param.Title,
                Items = this.mapper.Map<IEnumerable<CatalogItemViewModel>>(requestResult.Books),
            });
        }

    }
}
