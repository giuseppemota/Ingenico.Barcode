using Ingenico.Barcode.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Ingenico.Barcode.IoC;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
        _environment = environment;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await CreateInitialUserAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task CreateInitialUserAsync()
    {
        var email = _configuration["BootstrapUser:Email"];
        var password = _configuration["BootstrapUser:Password"];

        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
        {
            if (!_environment.IsDevelopment())
            {
                throw new InvalidOperationException(
                    "BootstrapUser:Email and BootstrapUser:Password must be configured outside Development.");
            }

            return;
        }

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "BootstrapUser:Email and BootstrapUser:Password must both be configured.");
        }

        if (await _userManager.FindByEmailAsync(email) is not null)
        {
            _logger.LogInformation("Initial user {Email} already exists.", email);
            return;
        }

        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Code));
            throw new InvalidOperationException($"Could not create the initial user: {errors}");
        }

        _logger.LogInformation("Created initial user {Email}.", email);
    }
}
