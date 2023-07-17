using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Requests.PublisherRequests;
using APMLibrary.Web.ViewModels.PublisherViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.PublisherPages
{
    [Authorize(Policy = "User")]
    public class PublisherInfoModel : PageModel
    {
        private readonly IMediator mediator = default!;
        private readonly IMapper mapper = default!;

        public int ProfileId { get => int.Parse(this.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid)); }
        public PublisherInfoModel(IMediator mediator, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public IEnumerable<PublicationViewModel> Publications { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var requestResult = await this.mediator.Send(new GetPublisherInfoRequest(this.ProfileId));
                this.Publications = this.mapper.Map<IEnumerable<PublicationViewModel>>(requestResult);
            }
            catch (NotFoundException requestError)
            {
                return this.BadRequest(requestError.Message);
            }
            return this.Page();
        }
    }
}
