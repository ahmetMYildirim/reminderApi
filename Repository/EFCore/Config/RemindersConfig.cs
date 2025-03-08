using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EFCore.Config
{
    public class RemindersConfig : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(r => r.id);

            builder.Property(r => r.title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.description)
                .HasMaxLength(500);

            builder.Property(r => r.dueTime)
                .IsRequired();

            builder.Property(r => r.status)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.created_at);
            builder.Property(r => r.updated_at);

            builder.HasMany(r => r.ReminderTags)
                .WithOne(rt => rt.Reminder)
                .HasForeignKey(rt => rt.reminder_id);
        }
    }
}
