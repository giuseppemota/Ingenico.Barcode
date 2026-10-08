
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
            ValidateCloudinaryConfiguration(configuration);
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
            services.AddHttpClient();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            

            



            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
        }

        private static void ValidateCloudinaryConfiguration(IConfiguration configuration)
        {
            foreach (var key in new[]
            {
                "Cloudinary:CloudName",
                "Cloudinary:ApiKey",
                "Cloudinary:ApiSecret"
            })
            {
                if (string.IsNullOrWhiteSpace(configuration[key]))
                {
                    throw new InvalidOperationException($"{key} must be configured.");
                }
            }
        }

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
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("ConnectionStrings:DefaultConnection must be configured.");
                }

                options.UseNpgsql(connectionString);
            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration) {
            var jwtKey = configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey) || Encoding.UTF8.GetByteCount(jwtKey) < 32)
            {
                throw new InvalidOperationException("Jwt:Key must be configured with at least 32 bytes.");
            }

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
        }
    }
}
