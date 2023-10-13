using Mi.DataDriver;
using Mi.DataDriver.EntityFrameworkCore;
using Mi.Domain.Json;
using Mi.Domain.PipelineConfiguration;
using Mi.Domain.Services;
using Mi.Domain.Shared;
using Mi.Domain.Shared.Core;
using Mi.Domain.Shared.Models;
using Mi.Domain.Shared.Models.UI;
using Mi.Domain.User;
using Mi.Web.Host.Middleware;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Mi.Web.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
                opt.JsonSerializerOptions.Converters.Add(new DateTimeFormatConverter());
                opt.JsonSerializerOptions.Converters.Add(new DateTimeNullableFormatConverter());
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => builder.Configuration.Bind("CookieSettings", options));
            builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, FuncAuthorizationMiddleware>();

            PipelineStartup.Instance.ConfigureServices(builder.Services);
            ConfigureService(builder.Services, builder.Configuration);

            var app = builder.Build();

            App.Running(app.Environment.IsDevelopment(), app.Environment.WebRootPath, app.Configuration);

            ServiceManager.SetProvider(app.Services);
            PipelineStartup.Instance.Configure(app);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<UserMiddleware>();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute("api-router", "{controller=Home}/{action=Index}");

            app.Run();
        }

        private static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            // DB & Repository
            services.AddMiDbContext(configuration.GetConnectionString("Sqlite")!);
            services.AddRepository();
            //services.AddSingleton<IDictionaryApi, DictionaryService>();

            // CurrentUser
            services.AddCurrentUser();

            // cache
            services.AddMemoryCache();

            // AddAutomaticInjection
            services.AddAutomaticInjection();

            services.AddScoped<MiHeader>();

            // UI Config
            var uiConfig = configuration.GetSection("AdminUI");
            services.Configure<PaConfigModel>(uiConfig);
        }
    }
}