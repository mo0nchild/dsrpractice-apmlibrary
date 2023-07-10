using APMLibrary.Bll.Models;
using APMLibrary.Bll.Requests;
using APMLibrary.Bll.Requests.ProfileRequests;
using APMLibrary.Web.ViewModels.ProfileViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APMLibrary.Web.ViewComponents
{
    [ViewComponent]
    public partial class ProfileNavViewComponent : ViewComponent
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public ProfileNavViewComponent(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var userClaim = this.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
            if (userClaim == null || !int.TryParse(userClaim, out var profileId))
            {
                return this.View<ProfileNavViewModel>(null);
            }
            var requestResult = await this._mediator.Send(new GetProfileRequest(profileId));
            this.ViewBag.IsAdmin = this.HttpContext.User.HasClaim(ClaimTypes.Role, "Admin");

            return this.View(requestResult == null ? null 
                : this._mapper.Map<ProfileNavViewModel>(requestResult));
        }
    }
}
