using APMLibrary.Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace APMLibraryClient
{
    public class Program : object
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddRazorPages(options => options.RootDirectory = "/Pages");
            builder.Services.AddControllers();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/authorization");
                options.LoginPath = new PathString("/authorization");
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Reader", item => item.RequireClaim(ClaimTypes.Role, "Reader"));
                options.AddPolicy("Author", item => item.RequireClaim(ClaimTypes.Role, "Author"));

                options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, "Admin"));
            });
            builder.Services.AddDbContextFactory<LibraryDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration["DATABASE_STRING"]);
            });
            var application = builder.Build();

            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Error");
                application.UseHsts();
            }
            application.UseHttpsRedirection().UseStaticFiles();
            application.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString(""),
            });

            application.UseRouting().UseAuthentication().UseAuthorization();

            application.MapRazorPages();
            application.MapControllers();

            application.MapControllerRoute(name: "default", pattern: "{controller}/{action}", new { });
            application.Run();
        }
        public Program() : base() { }
    }
}