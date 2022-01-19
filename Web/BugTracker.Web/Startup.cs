namespace BugTracker.Web
{
    using System.Reflection;

    using BugTracker.Data;
    using BugTracker.Data.Common;
    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Data.Repositories;
    using BugTracker.Data.Seeding;
    using BugTracker.Services.Data;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels;
    using Markdig;
    using Markdig.Extensions.AutoIdentifiers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Westwind.AspNetCore.Markdown;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();


            //Test

            services.AddMarkdown(config =>
            {
                // optional Tag BlackList
                config.HtmlTagBlackList = "script|iframe|object|embed|form"; // default

                // Simplest: Use all default settings
                var folderConfig = config.AddMarkdownProcessingFolder("/docs/", "~/Pages/__MarkdownPageTemplate.cshtml");

                // Customized Configuration: Set FolderConfiguration options
                folderConfig = config.AddMarkdownProcessingFolder("/posts/", "~/Pages/__MarkdownPageTemplate.cshtml");

                // Optionally strip script/iframe/form/object/embed tags ++
                folderConfig.SanitizeHtml = false;  //  default

                // Optional configuration settings
                folderConfig.ProcessExtensionlessUrls = true;  // default
                folderConfig.ProcessMdFiles = true; // default

                // Optional pre-processing - with filled model
                folderConfig.PreProcess = (model, controller) =>
                {
                    // controller.ViewBag.Model = new MyCustomModel();
                };

                // folderConfig.BasePath = "https://github.com/RickStrahl/Westwind.AspNetCore.Markdow/raw/master";

                // Create your own IMarkdownParserFactory and IMarkdownParser implementation
                // to replace the default Markdown Processing
                //config.MarkdownParserFactory = new CustomMarkdownParserFactory();                 

                // optional custom MarkdigPipeline (using MarkDig; for extension methods)
                config.ConfigureMarkdigPipeline = builder =>
                {
                    builder.UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
                        .UsePipeTables()
                        .UseGridTables()
                        .UseAutoIdentifiers(AutoIdentifierOptions.GitHub) // Headers get id="name" 
                        .UseAutoLinks() // URLs are parsed into anchors
                        .UseAbbreviations()
                        .UseYamlFrontMatter()
                        .UseEmojiAndSmiley(true)
                        .UseListExtras()
                        .UseFigures()
                        .UseTaskLists()
                        .UseCustomContainers()
                        //.DisableHtml()   // renders HTML tags as text including script
                        .UseGenericAttributes();
                };
            });

            services.AddControllersWithViews()
                 .AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);

            //End Test

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //For markdown
            app.UseMarkdown();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
