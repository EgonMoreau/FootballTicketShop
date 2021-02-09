using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using webproject1920.Areas.Identity.Data;
using webproject1920.Data;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Repository;
using webproject1920.Repository.Interfaces;
using webproject1920.Service;
using webproject1920.Service.Interfaces;
using webproject1920.Util.Mail;

namespace webproject1920
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ticketproject1920Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IWebproject1920Context, ticketproject1920Context>();
            services.AddTransient<ICompetitionsService, CompetitionsService>();
            services.AddTransient<ICompetitionsDAO, CompetitionsDAO>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<ILocationsDAO, LocationsDAO>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IMatchDAO, MatchDAO>();
            services.AddTransient<IMatchStadiumLocationService, MatchStadiumLocationService>();
            services.AddTransient<IMatchStadiumLocationDAO, MatchStadiumLocationDAO>();
            services.AddTransient<IOrderLineService, OrderLineService>();
            services.AddTransient<IOrderLineDAO, OrderLineDAO>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderDAO, OrderDAO>();
            services.AddTransient<IStadiumLocationsService, StadiumLocationsService>();
            services.AddTransient<IStadiumLocationsDAO, StadiumLocationsDAO>();
            services.AddTransient<IStadiumService, StadiumService>();
            services.AddTransient<IStadiumDAO, StadiumDAO>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<ISubscriptionDAO, SubscriptionDAO>();
            services.AddTransient<ITeamsService, TeamsService>();
            services.AddTransient<ITeamsDAO, TeamsDAO>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketDAO, TicketDAO>();
            services.AddTransient<ITeamCompetitionLocationService, TeamCompetitionLocationService>();
            services.AddTransient<ITeamCompetitionLocationDAO, TeamCompetitionLocationDAO>();

            services.AddDefaultIdentity<ApplicationUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

            services.AddAutoMapper(typeof(Startup));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();

            //Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {

                options.Cookie.Name = "be.VIVES.webproject";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
