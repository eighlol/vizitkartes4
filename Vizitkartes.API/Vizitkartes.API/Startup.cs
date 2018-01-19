using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            var connectionString = Configuration["connectionStrings:vizitKartesDBConnectionString"];
            services.AddDbContext<VizitkartesContext>(o => o.UseSqlServer(connectionString));
            services.AddIdentity<VizitkartesUser, IdentityRole>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<VizitkartesContext>()
                .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                            return Task.FromResult<object>(null);
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult<object>(null);
                        }
                    }
                };
            });
            
            services.AddTransient<VizitkartesDbContextSeedData>();

            services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, VizitkartesDbContextSeedData vizitkartesDbContextSeedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseAuthentication();

            
            app.UseStatusCodePages();


            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.BusinessCard, Models.BusinessCardDto>().ReverseMap();
                cfg.CreateMap<Models.BusinessCardForUpdateDto, Entities.BusinessCard>();
                cfg.CreateMap<Entities.Company, Models.CompanyDto>();
                cfg.CreateMap<Entities.Department, Models.DepartmentDto>().ReverseMap();
                cfg.CreateMap<Entities.Office, Models.OfficeDto>().ReverseMap();
            });

            vizitkartesDbContextSeedData.EnsureSeedDataForContext().Wait();
        }
    }
}
