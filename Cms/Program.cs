using Autofac.Extensions.DependencyInjection;
using Autofac;
using Cms.AutofaceDI;
using Cms.EntityFramewrokCore;
using Cms.Filters;
using Cms.IRepository;
using Cms.IService;
using Cms.Repository;
using Cms.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cms.Profiles;
using Cms.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 添加DbContext
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    var connStr = builder.Configuration.GetConnectionString("StrConn");
    opt.UseSqlServer(connStr);
});
// 添加Filter
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<CheckSessionFilter>();
    opt.Filters.Add<UserInfoFilter>();
});
/*builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();*/
// 替换容器，初始化一个Autofac的示例。
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModuleRegister());
});

builder.Services.AddAutoMapper(typeof(AutoCustomerProfile));
builder.Services.AddSession();
builder.Services.AddHttpClient();

var app = builder.Build();

//处理文章点击的次数
void StartProcessArticelQueue()
{
    Task.Run(async () =>
    {
        while (true)
        {
            long articelId = 0;
            if (QueueHelper.QueueDequeue(out articelId))
            {
                var myDbContext = app!.Services.GetService<MyDbContext>();
                var articel = await myDbContext.ArticelInfos.Where(a => a.Id == articelId).FirstOrDefaultAsync();
                if (articel != null)
                {
                    articel.Changes = articel.Changes + 1;
                    await myDbContext.SaveChangesAsync();
                }
            }
            else
            {
                //停顿五秒钟，然后再查看队列中是否有数据，防止CPU空转
                await Task.Delay(5000);
            }
        }
    });
}
StartProcessArticelQueue();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
