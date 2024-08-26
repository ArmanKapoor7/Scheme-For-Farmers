using Microsoft.EntityFrameworkCore;
using SchemeForFarmers.Models;
using SchemeForFarmers.Areas.Bidder.Hubs;
using SchemeForFarmers.Areas.Bidder.MiddlewareExtensions;
using SchemeForFarmers.Areas.Bidder.Repositories;
using SchemeForFarmers.Areas.Bidder.SubscribeTableDependencies;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("dbcs");
builder.Services.AddDbContext<SchemeForFarmersContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeBidTableDependency>();

builder.Services.AddRazorPages();

//var provider = builder.Services.BuildServiceProvider();
//var config = provider.GetRequiredService<IConfiguration>();
//builder.Services.AddDbContext<SchemeForFarmersContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<DashboardHub>("/Bidder/dashboardHub");

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.UseSqlTableDependency<SubscribeBidTableDependency>(connectionString);

app.Run();
