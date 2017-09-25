using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Ideas.Api.Filters;
using Ideas.Api.IoC;
using Ideas.Api.Swagger.Filters;
using Ideas.DataAccess;
using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Settings;
using Ideas.Domain.Users.Commands;
using Ideas.Mailing;
using Ideas.Mailing.EventHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Ideas.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MailingSettings>(Configuration.GetSection("Mailing"));
            services.Configure<AuthSettings>(Configuration.GetSection("AuthSettings"));

            services.AddDbContext<IdeasDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdeasDb")));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;                
            })
            .AddEntityFrameworkStores<IdeasDbContext>()
            .AddDefaultTokenProviders();                       

            //register mediator and all commands, queries, events and handlers from assemblies
            services.AddMediatR(typeof(CreateUser).Assembly, //domain
                                typeof(EmailCreatedUser).Assembly); //mailing  

            services.AddAutoMapper();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.AddGlobalExceptionFilters();
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddApiVersioning(cfg =>
            {
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "Ideas API", Version = "v1.0" });

                c.DescribeAllEnumsAsStrings();

                c.DocInclusionPredicate((version, apiDesc) =>
                {
                    var actionVersions = apiDesc.ActionAttributes().OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    var controllerVersions = apiDesc.ControllerAttributes().OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);

                    var controllerAndActionVersionsOverlap = controllerVersions.Intersect(actionVersions).Any();
                    if (controllerAndActionVersionsOverlap)
                    {
                        return actionVersions.Any(v => $"v{v.ToString()}" == version);
                    }
                    return controllerVersions.Any(v => $"v{v.ToString()}" == version);
                });

                c.OperationFilter<RemoveVersionParametersFilter>();
                c.DocumentFilter<SetVersionInPathsFilter>();
            });

            //create autofac container builder
            var containerBuilder = new ContainerBuilder();

            //register autofac modules
            containerBuilder.RegisterModule<DataAccessModule>();
            containerBuilder.RegisterModule<DomainServicesModule>();
            containerBuilder.RegisterModule<MailingModule>();

            //populate autofac container with Asp.Net dependencies
            containerBuilder.Populate(services);

            //create autofac container and service provider
            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Ideas API v1.0");
                c.ShowRequestHeaders();
                c.ShowJsonEditor();                
            });
        }
    }

    public class TestSettings
    {
        public string Text { get; set; }

        public int Value { get; set; }
    }
}
