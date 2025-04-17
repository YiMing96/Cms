using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Cms.Entity
{
    public class UserInfoConfig : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("T_UserInfos");
            builder.Property(u => u.UserName).HasMaxLength(20).IsRequired();
            builder.Property(u => u.UserPassword).HasMaxLength(20).IsRequired();
            builder.Property(u => u.UserEmail).HasMaxLength(120).IsRequired();
            builder.Property(u => u.UserPhone).HasMaxLength(11).IsRequired();
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.PhotoUrl).HasMaxLength(300);


        }
    }
}
