using AmalgamateLabs.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmalgamateLabs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using var client = new SQLiteDBContext();
            client.Database.EnsureCreated();
            Seed.InitiateSeed(client).Wait();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddEntityFrameworkSqlite().AddDbContext<SQLiteDBContext>();

            services.AddTransient<AppLogger, AppLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}"); /*stackoverflow.com/questions/46373668/when-using-usestatuscodepageswithreexecute-statuscode-is-not-sent-to-browser*/
            app.UseHsts();

            // docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache?view=aspnetcore-3.0
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "Blog/{id}/{*urlsafetitle}", //TODO: This seems to let any string after the id/ to work, not just the urlsafetitle. Check out: https://www.hanselman.com/blog/ASPNETCore22ParameterTransformersForCleanURLGenerationAndSlugsInRazorPagesOrMVC.aspx
                    defaults: new { controller = "Blog", action = "Details" });
            });

            #region Unity
            FileExtensionContentTypeProvider contentTypeProvider = new FileExtensionContentTypeProvider();
            // stackoverflow.com/questions/35288490/unity-webgl-js-file-not-found
            // Answer by Tengku Fathullah
            contentTypeProvider.Mappings[".mem"] = "application/octet-stream";
            contentTypeProvider.Mappings[".memgz"] = "application/octet-stream";
            contentTypeProvider.Mappings[".data"] = "application/octet-stream";
            contentTypeProvider.Mappings[".datagz"] = "application/octet-stream";
            contentTypeProvider.Mappings[".jsgz"] = "application/x-javascript; charset=UTF-8";

            // If not for Unity, I would still call UseStaticFile(), but with no parameters and up above the call to UseCookiePolicy().
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = contentTypeProvider
            });
            #endregion
        }
    }
}
