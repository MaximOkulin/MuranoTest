using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Services;
using SearchEngine.Business.Settings;
using SearchEngine.DataAccess;
using SearchEngine.DataAccess.Interfaces;
using SearchEngine.DataAccess.Repositories;
using SearchEngine.Models;
using System.Net.Http;

namespace SearchEngine
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddDbContext<SearchEngineContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString(StringResources.DatabaseName),
                x => x.MigrationsAssembly(StringResources.SearchEngineWeb)));

            services.AddScoped<IRepository, SQLSearchRepository>();

            
            services.Configure<GoogleSettings>(Configuration.GetSection(StringResources.GoogleSettings));
            services.Configure<YandexSettings>(Configuration.GetSection(StringResources.YandexSettings));
            services.Configure<BingSettings>(Configuration.GetSection(StringResources.BingSettings));

            services.TryAddTransient(s =>
            {
                return s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty);
            });

            services.AddSingleton<IEngine, GoogleEngine>();
            services.AddSingleton<IEngine, YandexEngine>();
            services.AddSingleton<IEngine, BingEngine>();

            services.AddHttpClient<GoogleEngine>();
            services.AddHttpClient<YandexEngine>();
            services.AddHttpClient<BingEngine>();

            services.AddScoped<ISearcher, Searcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
