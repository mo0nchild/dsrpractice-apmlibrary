using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Web.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace APMLibrary.Web.Pages.ProfilePages
{
    [ValidateAntiForgeryToken]
    public partial class RegistrationModel : PageModel
    {
        protected readonly IMediator _mediator = default!;
        protected readonly IMapper _mapper = default!;

        public RegistrationModel(IMapper mapper, IMediator mediator) : base()
            => (this._mapper, this._mediator) = (mapper, mediator);

        public async Task<IActionResult> OnGetAsync()
        {
            if (this.HttpContext.User.Identity != null && this.HttpContext.User.Identity.IsAuthenticated)
            {
                return this.RedirectToPage("/ProfilePages/ProfileInfo");
            }
            return this.Page();
        }

        [FromForm, BindProperty(Name = "RegisterModel")]
        public RegistrationViewModel RegisterModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid) return this.Page();
            var mappingRequest = this._mapper.Map<CreateProfileCommand>(this.RegisterModel);
            try 
            { 
                var requestResult = await this._mediator.Send(mappingRequest);
                var profileIdentity = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.PrimarySid, requestResult.ToString()),
                    new Claim(ClaimTypes.IsPersistent, this.RegisterModel.Authorization.RememberMe.ToString())

                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await this.HttpContext.SignInAsync(new ClaimsPrincipal(profileIdentity));
            }
            catch(CreateProfileException requestError)
            {
                this.ModelState.AddModelError("Login", requestError.Message);
                return this.Page();
            }
            this.HttpContext.Session.SetString("RememberMe", "");
            return this.RedirectToPage("/ProfilePages/ProfileInfo");
        }
    }
}
