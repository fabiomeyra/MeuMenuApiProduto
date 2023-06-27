using MeuMenu.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuMenu.Api.Configurations;

public static class DbContextConfiguration
{
    public static void AddDbContextConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<MeuMenuDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MeuMenuDb"));
        });
    }
}