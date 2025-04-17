using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ImageServerInfoConfig : IEntityTypeConfiguration<ImageServerInfo>
    {
        public void Configure(EntityTypeBuilder<ImageServerInfo> builder)
        {
            builder.ToTable("T_ImageServerInfos");
            builder.Property(i => i.ServerUrl).HasMaxLength(200).IsRequired();
            builder.Property(i => i.ServerName).HasMaxLength(100).IsRequired();
        }
    }
}
