using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using MyWorld.Services;
using MyWorld.Services.Interfaces;

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
            services.AddMvc();
            
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IAppSettingService, AppSettingService>();
            
#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();            
#else
            services.AddScoped<IMailService, MailService>();            
#endif           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            
            app.UseIISPlatformHandler();
                           
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseRuntimeInfoPage("/info"); 
            
            app.UseFileServer();    // Includes both middlewares below in the right order 
            //app.UseDefaultFiles();
            //app.UseStaticFiles();   // Files in wwwroot like images, html pages .etc

            // Usually put this after serving static files.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
                    // defaults: new { controller = "App", action = "Index" };
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
