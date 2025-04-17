using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ImageInfoConfig : IEntityTypeConfiguration<ImageInfo>
    {
        public void Configure(EntityTypeBuilder<ImageInfo> builder)
        {
            builder.ToTable("T_ImageInfos");
            builder.Property(i => i.ImageName).HasMaxLength(200).IsRequired();

        }
    }
}
