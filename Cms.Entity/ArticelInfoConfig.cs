using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ArticelInfoConfig : IEntityTypeConfiguration<ArticelInfo>
    {
        public void Configure(EntityTypeBuilder<ArticelInfo> builder)
        {
            builder.ToTable("T_ArticelInfos");
            builder.Property(x => x.KeyWords).HasMaxLength(500).IsRequired();
            builder.Property(x => x.TitleType).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FullTitle).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Intro).HasMaxLength(500).IsRequired();
            builder.Property(x => x.TitleFontColor).HasMaxLength(50).IsRequired();
            builder.Property(x => x.TitleFontType).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ArticelContent).IsRequired();
            builder.Property(x => x.Changes).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Author).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Origin).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PhotoUrl).HasMaxLength(500).IsRequired();
            // 一个类别下面多篇文章
            builder.HasOne<ArticelClass>(c => c.ArticelClass).WithMany(a => a.ArticelInfos).IsRequired().HasForeignKey(a=>a.ArticelClassId);
            

        }
    }
}
