using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EFCore.Config
{
    public class reminder_TagConfig : IEntityTypeConfiguration<Reminder_Tag>
    {
        public void Configure(EntityTypeBuilder<Reminder_Tag> builder)
        {
            builder.HasKey(rt => new {rt.reminder_id,rt.tag_id });

            builder.HasOne(r => r.Reminder)
                .WithMany(rt => rt.ReminderTags)
                .HasForeignKey(rt => rt.reminder_id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Tag)
                .WithMany(rt => rt.ReminderTags)
                .HasForeignKey(rt => rt.tag_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
