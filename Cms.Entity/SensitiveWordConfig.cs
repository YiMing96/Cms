using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class SensitiveWordConfig : IEntityTypeConfiguration<SensitiveWord>
    {
        public void Configure(EntityTypeBuilder<SensitiveWord> builder)
        {
            builder.ToTable("T_SensitiveWords");
            builder.Property(s => s.WordPattern).HasMaxLength(128).IsRequired();
            builder.Property(s => s.ReplaceWord).HasMaxLength(128);
        }
    }
}
