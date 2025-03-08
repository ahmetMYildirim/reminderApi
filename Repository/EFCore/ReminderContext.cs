using Entities;
using Entities.Auth_Models;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Repository.EFCore
{
    public class ReminderContext : IdentityDbContext<User>
    {
        public ReminderContext(DbContextOptions<ReminderContext> options) : base(options) { }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Reminder_Tag> ReminderTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Reminder>()
                 .HasOne(r => r.user_dto)
                 .WithMany()
                 .HasForeignKey(r => r.user_id)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
