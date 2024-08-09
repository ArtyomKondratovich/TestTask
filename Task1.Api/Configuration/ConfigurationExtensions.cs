using Microsoft.EntityFrameworkCore;
using Task1.Business.Services.Interfaces;
using Task1.Business.Services.Implementations;
using Task1.DataAccess;
using Task1.DataAccess.Repositories.Implementations;
using Task1.DataAccess.Repositories.Interfaces;

namespace Task1.Api.Configuration
{
    public  static class ConfigurationExtensions
    {
        public static void ConfigureDataAcess(this WebApplicationBuilder builder) 
        {
            builder.Services
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(
                        builder.Configuration.GetConnectionString("PostgreSQL")
                    )
                );

            builder.Services
                .AddScoped<ICurrencyRepository>(
                options => new CurrencyRepository(options.GetRequiredService<ApplicationDbContext>())
                );
        }

        public static void ConfigureBusiness(this WebApplicationBuilder builder) 
        {
            builder.Services.AddScoped<ICurrencyService>(
                options => new CurrencyService(options.GetRequiredService<ICurrencyRepository>(),
                builder.Configuration.GetSection("BankApiUrl").Get<string>())
            );
        }
    }
}
