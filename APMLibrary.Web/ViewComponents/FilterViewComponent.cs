using APMLibrary.Bll.Requests.SupportRequests;
using APMLibrary.Web.ViewModels.ComponentsViewModels;
using APMLibrary.Web.ViewModels.SupportViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APMLibrary.Web.ViewComponents
{
    [ViewComponent]
    public partial class FilterViewComponent : ViewComponent
    {
        protected readonly IMediator mediator = default!;
        protected readonly IMapper mapper = default!;

        public FilterViewComponent(IMediator mediator, IMapper mapper) : base()
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync(FilterViewModel viewModel, 
            string page, string? handler = default)
        {
            this.ViewBag.Handler = handler;
            this.ViewBag.Page = page;

            this.ViewBag.Languages = this.mapper.Map<IEnumerable<LanguageViewModel>>(
                await this.mediator.Send(new GetLanguagesRequest()));

            return this.View(viewModel);
        }
    }
}
