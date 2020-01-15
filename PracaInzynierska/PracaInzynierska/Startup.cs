using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracaInzynierska.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PracaInzynierska.Models.Entities;
using PracaInzynierska.Infrastructure;
using Microsoft.AspNetCore.Identity.UI.Services;
using PracaInzynierska.Hubs;


namespace PracaInzynierska
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

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMyEmailSender, MailManager>();

            services.AddSignalR();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        private async Task CreateMainAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var mainAdmin = new ApplicationUser
            {
                UserName = Configuration.GetSection("MainAdminSettings")["Email"],
                Email = Configuration.GetSection("MainAdminSettings")["Email"],
                FirstName = Configuration.GetSection("MainAdminSettings")["FirstName"],
                LastName = Configuration.GetSection("MainAdminSettings")["LastName"],
                City = Configuration.GetSection("MainAdminSettings")["City"],
                Street = Configuration.GetSection("MainAdminSettings")["Street"],
                ZIPCode = Configuration.GetSection("MainAdminSettings")["ZIPCode"],
                HouseNumber = Configuration.GetSection("MainAdminSettings")["HouseNumber"],
                PhoneNumber = Configuration.GetSection("MainAdminSettings")["PhoneNumber"],
            };

            string UserPassword = Configuration.GetSection("MainAdminSettings")["Password"];
            var _user = await userManager.FindByEmailAsync(Configuration.GetSection("MainAdminSettings")["Email"]);
            if (_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(mainAdmin, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(mainAdmin, "Admin");
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseSignalR(routes =>
            {
                routes.MapHub<QueueHub>("/QueueHub");
            });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
           name: "Menu",
           template: "Menu/{category?}",
           defaults: new { controller = "Product", action = "Products" });


              routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
            CreateMainAdmin(serviceProvider).Wait();
        }
    }
}
