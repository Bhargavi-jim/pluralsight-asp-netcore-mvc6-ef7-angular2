using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using MyWorld.Common.Profiles;
using MyWorld.Data;
using MyWorld.Data.Repository;
using MyWorld.Services;
using MyWorld.Services.Interfaces;
using MyWorld.Data.Storage;
using MyWorld.Data.Seed;
using Microsoft.AspNet.Identity.EntityFramework;
using MyWorld.Data.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authentication.Cookies;
using System.Threading.Tasks;
using System.Net;

namespace MyWorld
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IApplicationEnvironment env)//IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
                
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(config =>
                    {
#if !DEBUG
                        config.Filters.Add(new RequireHttpsAttribute());    // Redirect to https. We don't want to send anything not protected over the wire!
#endif                                    
                    })
                    .AddJsonOptions(opt => 
                    {
                        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });
            
            services.AddIdentity<WorldUser, IdentityRole>(config => 
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                
                /*
                 * Web API
                 * Return 401 Unauthorized instead of the html when the called by javascript 
                 */ 
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()  //Callbacks that Identity system allow us to override to change default behaviour
                {
                    // Only ever executed when authentication cookie already knows that redirect was in order 
                    // where an unauthorized call is being redirected. 
                    OnRedirectToLogin = ctx => 
                    {
                        if(ctx.Request.Path.StartsWithSegments("/api") && 
                           ctx.Response.StatusCode == (int) HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }                         
                        else 
                        {
                            // Default behaviour
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        
                        return Task.FromResult(0);
                    }
                };
                
                //config.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                //config.Cookies.ApplicationCookie.AutomaticChallenge = false;
                //config.Cookies.ApplicationCookieAuthenticationScheme = "ApplicationCookie";
            })
            .AddEntityFrameworkStores<WorldContext>();  // Store identity entities in WorldContext
            
            /* Web api redirect when authorization via [Authorize] attribute fails */            
            // services.Configure<IdentityOptions>(options=>
            // {
            //     options.Cookies.ApplicationCookie.LoginPath = new Microsoft.AspNet.Http.PathString("/Auth/Login");
            // });            
            // services.ConfigureCookieAuthentication(config =>
            // {
            //     config.LoginPath = "/Auth/Login";
            // });
            
            services.AddLogging();
            
            // services.AddEntityFramework()
            //         .AddSqlServer()
            //         .AddDbContext<WorldContext>(o => o.UseSqlServer(Configuration["Database:Connection"]));
            
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IAppSettingService, AppSettingService>();
            services.AddSingleton<IWorldStorage, WorldStorage>();
            
            /* Uncomment this to enable EF */
            //services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddScoped<IWorldRepository, FakeWorldRepository>();
            
#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();            
#else
            services.AddScoped<IMailService, MailService>();            
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)//, WorldContextSeedData seeder)
        {            
            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();            
            //app.UseGlobalExceptionHandler();                          
            
            app.UseIdentity();
            
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug(LogLevel.Debug);
 
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            //}
            
            app.UseRuntimeInfoPage("/info"); 
            
            app.UseFileServer();    // Includes both middlewares below in the right order 
            //app.UseDefaultFiles();
            //app.UseStaticFiles();   // Files in wwwroot like images, html pages .etc

            Mapper.Initialize(config => 
            {
                config.AddProfile<TripProfile>();
                config.AddProfile<StopProfile>();
            });
            
            // Usually put this after serving static files.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
                    // defaults: new { controller = "App", action = "Index" };
            });
            
            //await seeder.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}