using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cms.EntityFramewrokCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ArticelInfo> ArticelInfos { get; set; }
        public DbSet<ArticelClass> ArticelClasses { get; set; }
        public DbSet<ArticelComment> ArticelComments { get; set; }
        public DbSet<SensitiveWord> SensitiveWords { get; set; }
        public DbSet<ImageInfo> ImageInfos { get; set; }
        public DbSet<ImageServerInfo> ImageServers { get; set; }
        public DbSet<VideoInfo> VideoInfos { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cms.Entity.dll"));

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(type.Assembly);
            }

        }
    }
}
