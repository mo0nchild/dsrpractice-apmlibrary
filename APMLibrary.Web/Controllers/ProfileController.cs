using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace APMLibrary.Web.Controllers
{
    [Controller, Route("profile")]
    public partial class ProfileController : ControllerBase
    {
        protected readonly ILogger<ProfileController> _logger = default!;
        public ProfileController(ILogger<ProfileController> logger) : base() => this._logger = logger;

        [HttpGet, Route("logout", Name = "logout")]
        public async virtual Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToPage("/ProfilePages/Login");
        }
    }
}
