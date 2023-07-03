using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using System.Runtime.InteropServices;
using System.Security.Claims;
using static APMLibrary.Web.Middlewares.ProfileMiddleware;

namespace APMLibrary.Web.Middlewares
{
    public partial class ProfileMiddleware : object
    {
        protected readonly RequestDelegate _nextMiddleware = default!;
        protected readonly ProfileMiddlewareOptions _profileOptions = default!;

        public ProfileMiddleware(RequestDelegate next, ProfileMiddlewareOptions options) : base() 
            => (this._nextMiddleware, this._profileOptions) = (next, options);

        public sealed class ProfileMiddlewareOptions : object
        {
            public string RememberCookieName { get; set; } = default!;
            public ProfileMiddlewareOptions() : base() { }
        }

        public async virtual Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var rememberStatus = context.User.FindFirst(ClaimTypes.IsPersistent);
                if (rememberStatus != null && bool.TryParse(rememberStatus.Value, out var status) && !status)
                {
                    if (!context.Session.Keys.Contains(this._profileOptions.RememberCookieName))
                        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
            await this._nextMiddleware.Invoke(context);
        }
    }
    public static class ProfileMiddlewareExtension : object
    {
        public static readonly string DefaultCookieName = string.Format("RememberUser");

        public static IApplicationBuilder UseProfileMiddleware(this IApplicationBuilder builder,
            Action<ProfileMiddleware.ProfileMiddlewareOptions>? profileOptions = default) 
        {
            var profileMiddlewareOptions = new ProfileMiddleware.ProfileMiddlewareOptions()
            {
                RememberCookieName = ProfileMiddlewareExtension.DefaultCookieName,
            };
            profileOptions?.Invoke(profileMiddlewareOptions);

            return builder.UseMiddleware<ProfileMiddleware>(profileMiddlewareOptions);
        }
    }
}
