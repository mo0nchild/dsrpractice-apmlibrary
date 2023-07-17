using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.ProfileRequests;
using APMLibrary.Bll.Requests.PublisherRequests;
using APMLibrary.Web.ViewModels.BookViewModels;
using APMLibrary.Web.ViewModels.PublisherViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APMLibrary.Web.Pages.LibraryPages
{
    public class PublisherCatalogModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        public PublisherCatalogModel(IMediator mediator, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int PublisherId { get; set; } = default!;

        public IEnumerable<PublicationViewModel> Publications { get; set; } = default!;
        public PublisherViewModel Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var requestResult = await this.mediator.Send(new GetPublisherInfoRequest(this.PublisherId));
                this.Publications = this.mapper.Map<IEnumerable<PublicationViewModel>>(requestResult.Where(item => item.Published));

                this.Publisher = this.mapper.Map<PublisherViewModel>(await this.mediator.Send(new GetProfileRequest(this.PublisherId)));
            }
            catch (NotFoundException requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }
    }
}
