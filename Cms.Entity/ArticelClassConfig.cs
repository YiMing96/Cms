using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ArticelClassConfig : IEntityTypeConfiguration<ArticelClass>
    {
        public void Configure(EntityTypeBuilder<ArticelClass> builder)
        {
            builder.ToTable("T_ArticelClasses");
            builder.Property(x => x.ArticelClassName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(200).IsRequired();

        }
    }
}
