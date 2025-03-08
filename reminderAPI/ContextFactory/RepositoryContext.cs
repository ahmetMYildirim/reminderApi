using Microsoft.EntityFrameworkCore.Design;

namespace reminderAPI.ContextFactory
{
    public class RepositoryContext : IDesignTimeDbContextFactory<ReminderContext>
    {
        public ReminderContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
