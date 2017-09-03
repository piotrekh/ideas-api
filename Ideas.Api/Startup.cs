using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ideas.Api.IoC;
using Ideas.Api.Filters;
using Ideas.Mailing;
using Ideas.Mailing.EventHandlers;

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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.AddGlobalExceptionFilters();
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            services.Configure<MailingSettings>(Configuration);

            //register mediator and all commands, queries, events and handlers from assemblies
            services.AddMediatR(typeof(CreateUser).Assembly, //domain
                                typeof(EmailCreatedUser).Assembly); //mailing          

            //create autofac container builder
            var builder = new ContainerBuilder();

            //register autofac modules
            builder.RegisterModule<DomainServicesModule>();
            builder.RegisterModule<MailingModule>();

            //populate autofac container with Asp.Net dependencies
            builder.Populate(services);

            //create autofac container and service provider
            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
