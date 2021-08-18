namespace DavidsList
{
    using DavidsList.Data;
    using DavidsList.Services;
    using DavidsList.Data.DbModels;
    using DavidsList.Infrastructures;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using DavidsList.Services.Interfaces;
    using AspNetCoreHero.ToastNotification;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DavidsListDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                    ).ConfigureWarnings(x => x.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning)));

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 5;

                })
                .AddEntityFrameworkStores<DavidsListDbContext>();
            services.AddControllersWithViews();
            services.AddTransient<IGetInformationFromApi, GetInformationFromApi>();
            services.AddNotyf(config => { config.DurationInSeconds = 8; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountInteractor,AccountInteractor>();
            services.AddTransient<IMarkMovieService, MarkMovieService>();
            services.AddTransient<IDatabaseInteractor, DatabaseInteractor>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

        }
    }
}
