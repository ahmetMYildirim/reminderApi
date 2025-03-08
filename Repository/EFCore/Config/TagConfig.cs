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
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.id);
            builder.Property(t => t.tagName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(t => t.ReminderTags)
                .WithOne(rt => rt.Tag)
                .HasForeignKey(rt => rt.tag_id);
        }
    }
}
