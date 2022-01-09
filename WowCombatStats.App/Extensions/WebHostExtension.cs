using Microsoft.EntityFrameworkCore;
using WowCombatStats.Data;
using WowCombatStats.Domain;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WowCombatStats.App.Extensions
{
    public static class WebHostExtension
    {
        public static WebApplication Migrate(this WebApplication webApplication)
        {
            using (var sp = webApplication.Services.CreateScope())
            {
                var context = sp.ServiceProvider.GetService<DataContext>();
                context.Database.Migrate();
            }

            return webApplication;
        }

        public static WebApplication SeedingData(this WebApplication webApplication)
        {
            using (var serviceScope = webApplication.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                var hostingEnvironment = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();

                if (hostingEnvironment.EnvironmentName == "Development")
                {
                    DataSeeder.InitData(context);
                }
            }

            return webApplication;
        }
    }
}
