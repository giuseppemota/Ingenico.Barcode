
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentValidation;
using Ingenico.Barcode.Data;
using Ingenico.Barcode.Data.Repositorios;
using Ingenico.Barcode.Domain.Pipelines;
using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            ConfigureJWT(services, configuration);

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IImageUploadService, ImageUploadService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            

            



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
                //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration) {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configurar JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        }
    }
}
