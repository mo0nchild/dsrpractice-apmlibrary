using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Requests;
using APMLibrary.Web.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.ProfilePages
{
    [ValidateAntiForgeryToken]
    public partial class LoginModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public LoginModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public async Task<IActionResult> OnGetAsync()
        {
            if (this.HttpContext.User.Identity != null && this.HttpContext.User.Identity.IsAuthenticated)
            {
                return this.RedirectToPage("/ProfilePages/ProfileInfo");
            }
            return this.Page();
        }

        [FromForm, BindProperty(Name = "AuthorizationModel")]
        public LoginViewModel AuthorizationModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid) return this.Page();
            var mappingRequest = this._mapper.Map<GetAuthorizationRequest>(this.AuthorizationModel);

            var requestResult = await this._mediator.Send(mappingRequest);
            if(requestResult == null)
            {
                this.ModelState.AddModelError("AuthorizationModel.Login", "Пользователь не найден");
                return this.Page();
            }
            var profileIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, requestResult.ToString()!),
                new Claim(ClaimTypes.IsPersistent, this.AuthorizationModel.RememberMe.ToString())

            }, CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContext.SignInAsync(new ClaimsPrincipal(profileIdentity));
            return this.RedirectToPage("/ProfilePages/ProfileInfo");
        }
    }
}
