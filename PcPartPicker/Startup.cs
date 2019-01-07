using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PcPartPicker.Services.Interfaces;
using PcPartPicker.Services.Implementations;
using PcPartPicker.Data.Interfaces;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;

namespace PcPartPicker
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

            services.AddDbContext<PcPartPickerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<PcPartPickerDbContext>();

            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<ICpuService, CpuService>();
            services.AddScoped<IGpuService, GpuService>();
            services.AddScoped<IMotherboardService, MotherboardService>();
            services.AddScoped<IMemoryOptionService, MemoryOptionService>();
            services.AddScoped<IStorageOptionService, StorageOptionService>();
            services.AddScoped<ISystemBuildService, SystemBuildService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Case>, Repository<Case>>();
            services.AddScoped<IRepository<Cpu>, Repository<Cpu>>();
            services.AddScoped<IRepository<Gpu>, Repository<Gpu>>();
            services.AddScoped<IRepository<Motherboard>, Repository<Motherboard>>();
            services.AddScoped<IRepository<MemoryOption>, Repository<MemoryOption>>();
            services.AddScoped<IRepository<StorageOption>, Repository<StorageOption>>();
            services.AddScoped<IRepository<SystemBuild>, Repository<SystemBuild>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //     name: "areaRoute",
                //     template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "componentsRoute",
                    areaName: "Component",
                    template: "Component/{controller=Home}/{action=Index}/{id?}"
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Vendor", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("admin@admin1.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@admin1.com",
                    Email = "admin@admin1.com",
                };
                await UserManager.CreateAsync(user, "Password1.");
            }
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
