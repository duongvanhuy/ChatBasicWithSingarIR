using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VFT.ChartSingalR.Areas.Identity.Data;
using VFT.ChartSingalR.Data;
using VFT.ChartSingalR.Hubs;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VFTChartSingalRContextConnection") ?? throw new InvalidOperationException("Connection string 'VFTChartSingalRContextConnection' not found.");

builder.Services.AddDbContext<VFTChartSingalRContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<VFTChartSingalRUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<VFTChartSingalRContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");
app.Run();
