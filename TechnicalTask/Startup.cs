using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;

using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Integration.AspNetCore;

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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
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

            services.AddMvc();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));
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

            //_container.Register<CustomMiddleware>();

            _container.Verify();

            // Add custom middleware
            //app.Use(async (httpContext, next) =>
            //{
            //    await _container.GetInstance<CustomMiddleware>().Invoke(httpContext, next);
            //});

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Users}/{action=Get}/{id?}");
            });

            DbInitializer.Initialize(context);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            _container.Register<IRepository<User>, Repository<User>>(Lifestyle.Scoped);
            _container.Register<IRepository<Country>, Repository<Country>>(Lifestyle.Scoped);
            _container.Register<IRepository<Organization>, Repository<Organization>>(Lifestyle.Scoped);
            _container.Register<IRepository<Business>, Repository<Business>>(Lifestyle.Scoped);
            _container.Register<IRepository<Family>, Repository<Family>>(Lifestyle.Scoped);
            _container.Register<IRepository<Offering>, Repository<Offering>>(Lifestyle.Scoped);
            _container.Register<IRepository<Department>, Repository<Department>>(Lifestyle.Scoped);

            //_container.Register<ILogger>(Lifestyle.Scoped);

            //_container.Register(typeof(IRepository<>), new[] { typeof(IRepository<>).Assembly });

            // Cross-wire ASP.NET services (if any). For instance:
            _container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());
            // NOTE: Prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }
}
