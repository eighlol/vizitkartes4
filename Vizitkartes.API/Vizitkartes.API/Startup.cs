using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vizitkartes.API.Entities;
using Vizitkartes.API.Services;

namespace Vizitkartes.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:vizitKartesDBConnectionString"];
            services.AddDbContext<VizitkartesContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, VizitkartesContext vizitkartesContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            
            vizitkartesContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            app.UseMvc();
            
        }
    }
}
