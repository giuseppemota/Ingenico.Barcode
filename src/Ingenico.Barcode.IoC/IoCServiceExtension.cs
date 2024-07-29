
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Ingenico.Barcode.Data;
using Ingenico.Barcode.Domain.Pipelines;
using Ingenico.Barcode.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ingenico.Barcode.IoC

{
    [ExcludeFromCodeCoverage]
    public static class IoCServiceExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BehaviorValidation<,>));
            ConfigurarFluentValidation(services);



            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
            //ConfigurarRefit(services, configuration);
        }
        //private static void ConfigurarRefit(IServiceCollection services, IConfiguration configuration)
        //{

        //    services.
        //        AddRefitClient<IExternalBankHistory>().
        //        ConfigureHttpClient(httpClient =>
        //            httpClient.BaseAddress =
        //            new Uri("http://localhost:5039")
        //        );
        //}
        private static void ConfigurarFluentValidation(IServiceCollection services)
        {
            var abstractValidator = typeof(AbstractValidator<>);
            var validadores = typeof(Input)
                .Assembly
                .DefinedTypes
                .Where(type => type.BaseType?.IsGenericType is true &&
                type.BaseType.GetGenericTypeDefinition() ==
                abstractValidator)
                .Select(Activator.CreateInstance)
                .ToArray();

            foreach (var validator in validadores)
            {
                services.AddSingleton(validator!.GetType().BaseType!, validator);
            }
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }
    }
}
