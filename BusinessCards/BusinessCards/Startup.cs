using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessCards.Entities;
using BusinessCards.Filters;
using BusinessCards.Seed;
using BusinessCards.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BusinessCards
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = new PathString("/Auth/Login");
            //    options.Events = new CookieAuthenticationEvents()
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            //            {
            //                ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //                return Task.FromResult<object>(null);
            //            }
            //            else
            //            {
            //                ctx.Response.Redirect(ctx.RedirectUri);
            //                return Task.FromResult<object>(null);
            //            }
            //        }
            //    };
            //});
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddTransient<BusinessCardDbSeed>();
            //services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BusinessCardDbSeed dbSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.BusinessCard, Models.BusinessCardDto>().ReverseMap();
                cfg.CreateMap<Models.BusinessCardForUpdateDto, Entities.BusinessCard>();
                cfg.CreateMap<Entities.Company, Models.CompanyDto>()
                    .ForMember(dest => dest.Manager, opts => opts.MapFrom(src => src.Manager.BusinessCard));

                cfg.CreateMap<Entities.Department, Models.DepartmentDto>().ReverseMap();
                cfg.CreateMap<Entities.Office, Models.OfficeDto>().ReverseMap();
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
            dbSeed.EnsureSeed().Wait();
        }
    }
}
