using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Entity
{
    public class ArticelCommentConfig : IEntityTypeConfiguration<ArticelComment>
    {
        public void Configure(EntityTypeBuilder<ArticelComment> builder)
        {
            builder.ToTable("T_ArticelComments");
            builder.Property(a => a.Msg).IsRequired();
            // 配置一对多关系
            builder.HasOne(a => a.ArticelInfo).WithMany(a => a.ArticelComments).IsRequired();
        }
    }
}
