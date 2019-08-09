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
        private const string SEED_USERS_PASSWORD = "Qwerty123#";

        private const string USER_EMAIL = "user@gmail.com";
        private const string VENDOR_EMAIL = "vendor@gmail.com";
        private const string ADMIN_EMAIL = "admin@gmail.com";

        private const string USER_ROLE = "User";
        private const string VENDOR_ROLE = "Vendor";
        private const string ADMIN_ROLE = "Admin";

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
            string[] roleNames = { ADMIN_ROLE, VENDOR_ROLE, USER_ROLE };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync(USER_EMAIL);

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = USER_EMAIL,
                    Email = USER_EMAIL
                };
                await UserManager.CreateAsync(user, SEED_USERS_PASSWORD);
            }
            await UserManager.AddToRoleAsync(user, USER_ROLE);

            IdentityUser vendor = await UserManager.FindByEmailAsync(VENDOR_EMAIL);

            if (vendor == null)
            {
                vendor = new IdentityUser()
                {
                    UserName = VENDOR_EMAIL,
                    Email = VENDOR_EMAIL
                };
                await UserManager.CreateAsync(vendor, SEED_USERS_PASSWORD);
            }
            await UserManager.AddToRoleAsync(vendor, VENDOR_ROLE);

            IdentityUser admin = await UserManager.FindByEmailAsync(ADMIN_EMAIL);

            if (admin == null)
            {
                admin = new IdentityUser()
                {
                    UserName = ADMIN_EMAIL,
                    Email = ADMIN_EMAIL
                };
                await UserManager.CreateAsync(admin, SEED_USERS_PASSWORD);
            }
            await UserManager.AddToRoleAsync(admin, ADMIN_ROLE);
        }
    }
}
