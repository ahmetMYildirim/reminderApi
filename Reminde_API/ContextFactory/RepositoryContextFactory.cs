using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository.EFCore;

namespace Reminde_API.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<ReminderContext>
    {
        public ReminderContext CreateDbContext(string[] args)
        {
            var configration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<ReminderContext>().UseSqlServer(configration.GetConnectionString("sqlConnection"),
                prj => prj.MigrationsAssembly("Reminde_API"));

            return new ReminderContext(builder.Options);
        }
    }
}
