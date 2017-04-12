using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Integration.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using TechnicalTask.Services;

namespace TechnicalTask
{   
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<TtContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });

            //services.AddIdentity<User, IdentityRole>()
            //    .AddDefaultTokenProviders();

            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddMvc(options =>
            {
                options.SslPort = 44336;
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddDistributedMemoryCache();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Tt API", Version = "v1"});
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "TechnicalTask.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSimpleInjectorAspNetRequestScoping(_container);

            _container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();

            var options = new DbContextOptionsBuilder<TtContext>();
            options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            var context = new TtContext(options.Options);

            _container.Register(() => context, Lifestyle.Singleton);

            InitializeContainer(app);

            _container.Verify();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/testlog.txt");

            //app.UseIdentity();
            //app.UseGoogleAuthentication(new GoogleOptions()
            //{
            //    ClientId = Configuration["Authentication:Google:ClientId"],
            //    ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            //});

            //app.UseLinkedInAuthentication(new LinkedInOptions
            //{
            //    ClientId = null,
            //    ClientSecret = null
            //});

            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tt API V1");
            });

            // Add the cookie middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/login"),
                LogoutPath = new PathString("/logout")
            });


            var callbalck = new PathString("/signin-linkedin");
            // Add the OAuth2 middleware
            app.UseOAuthAuthentication(new OAuthOptions
            {
                AuthenticationScheme = "LinkedIn",
                ClientId = Configuration["Authentication:LinkedIn:ClientId"],
                ClientSecret = Configuration["Authentication:LinkedIn:ClientSecret"],
                CallbackPath = callbalck,
                AuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization",
                TokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken",
                UserInformationEndpoint = "https://api.linkedin.com/v1/people/~:(id,formatted-name,email-address,picture-url)",
                Scope = { "r_basicprofile"},
            });

            // Listen for requests on the /login path, and issue a challenge to log in with the LinkedIn middleware
            // Listen for requests on the /login path, and issue a challenge to log in with the LinkedIn middleware
            app.Map("/login", builder =>
            {
                builder.Run(async httpContext =>
                {
                    // Return a challenge to invoke the LinkedIn authentication scheme
                    await httpContext.Authentication.ChallengeAsync("LinkedIn", properties: new AuthenticationProperties() { RedirectUri = "/LoggedIn" });
                });
            });
            // Listen for requests on the /logout path, and sign the user out
            app.Map("/logout", builder =>
            {
                builder.Run(async httpContext =>
                {
                    // Sign the user out of the authentication middleware (i.e. it will clear the Auth cookie)
                    await httpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    // Redirect the user to the home page after signing out
                    httpContext.Response.Redirect("/LoggedOut");
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api /{controller=Users}/{action=Get}/{id?}");
            });

            DbInitializer.Initialize(context);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            //IServiceProvider provider = app.ApplicationServices;
            //_container.Register(provider.GetRequiredService<UserManager<User>>);
            //_container.Register(provider.GetRequiredService<SignInManager<User>>);


            //_container.Register<IUserStore<User>>(() => new UserStore<User>(_container.GetInstance<TtContext>()),Lifestyle.Scoped);
            //_container.RegisterInitializer<ApplicationUserManager>(manager => InitializeUserManager(manager, app));

            //_container.Register<UserManager<User>>(() => new UserManager<User>(new UserStore<User>()),Lifestyle.Scoped);

            //_container.Register<IUserStore<User>>(() => new UserStore<User>(_container.GetInstance<TtContext>()), Lifestyle.Scoped);
            //_container.Register<UserManager<User>>(Lifestyle.Scoped);

            _container.Register<IRepository<User>, Repository<User>>(Lifestyle.Scoped);
            _container.Register<IRepository<OrganizationCountry>, Repository<OrganizationCountry>>(Lifestyle.Scoped);
            _container.Register<IRepository<Country>, Repository<Country>>(Lifestyle.Scoped);
            _container.Register<IRepository<Organization>, Repository<Organization>>(Lifestyle.Scoped);
            _container.Register<IRepository<Business>, Repository<Business>>(Lifestyle.Scoped);
            _container.Register<IRepository<Family>, Repository<Family>>(Lifestyle.Scoped);
            _container.Register<IRepository<Offering>, Repository<Offering>>(Lifestyle.Scoped);
            _container.Register<IRepository<Department>, Repository<Department>>(Lifestyle.Scoped);

            _container.Register<IService<User>, UserService>(Lifestyle.Scoped);
            _container.Register<IService<Country>, CountryService>(Lifestyle.Scoped);
            _container.Register<IService<Organization>, OrganizationService>(Lifestyle.Scoped);
            _container.Register<IService<Business>, BusinessService>(Lifestyle.Scoped);
            _container.Register<IService<Family>, FamilyService>(Lifestyle.Scoped);
            _container.Register<IService<Offering>, OfferingService>(Lifestyle.Scoped);
            _container.Register<IService<Department>, DepartmentService>(Lifestyle.Scoped);

            // Cross-wire ASP.NET services (if any). For instance:
            _container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());
            // NOTE: Prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }
}
