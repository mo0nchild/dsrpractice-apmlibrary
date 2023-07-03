using APMLibrary.Dal;
using APMLibrary.Bll;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using APMLibrary.Web.Middlewares;
using APMLibrary.Web.Configurations;

namespace APMLibrary.Web
{
    public static class Program : object
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
            builder.Configuration.AddEnvironmentVariables();
            builder.Services.Configure<ListCatalogOptions>(builder.Configuration.GetSection("ListCatalog"));

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            builder.Services.AddControllers();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(10));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/profile/login");
                options.LoginPath = new PathString("/profile/login");
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", item => item.RequireClaim(ClaimTypes.Role, "User"));
                options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, "Admin"));
            });
            await builder.Services.AddDataAccessLayer(builder.Configuration);
            await builder.Services.AddBusinessLogicLayer(builder.Configuration);

            var application = builder.Build();

            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Error");
                application.UseHsts();
            }
            application.UseHttpsRedirection().UseStaticFiles().UseSession();
            application.UseProfileMiddleware(options => options.RememberCookieName = "RememberMe");

            application.UseRouting().UseAuthentication().UseAuthorization();
            application.MapRazorPages();
            application.MapControllers();

            application.MapControllerRoute(name: "default", pattern: "{controller}/{action}", new { });
            application.Run();
        }
    }
}