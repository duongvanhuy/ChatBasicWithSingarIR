using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VFT.ChartSingalR.Areas.Identity.Data;
using VFT.ChartSingalR.Models;

namespace VFT.ChartSingalR.Data;

public class VFTChartSingalRContext : IdentityDbContext<VFTChartSingalRUser>
{
   
    public VFTChartSingalRContext(DbContextOptions<VFTChartSingalRContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageContext> MessageContexts { get; set; }
}
