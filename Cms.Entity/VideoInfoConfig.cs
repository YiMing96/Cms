using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class VideoInfoConfig : IEntityTypeConfiguration<VideoInfo>
    {
        public void Configure(EntityTypeBuilder<VideoInfo> builder)
        {
            builder.ToTable("T_Videos");
            builder.Property(v => v.VideoName).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Author).HasMaxLength(100).IsRequired();
            builder.Property(v => v.FileSHA256Hash).IsRequired();
            builder.Property(v => v.FileSizeInBytes).IsRequired();
            builder.Property(v => v.VideoContent).IsRequired();
            builder.Property(v => v.VideoPath).IsRequired();
        }
    }
}
