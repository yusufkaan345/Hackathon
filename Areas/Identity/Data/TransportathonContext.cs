using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Transportathon.Areas.Identity.Data;
using Transportathon.Models;

namespace Transportathon.Data;

public class TransportathonContext : IdentityDbContext<IdentityUser>
{
    public TransportathonContext(DbContextOptions<TransportathonContext> options)
        : base(options)
    {
    }
 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        
    }
    
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u=>u.NameSurname).HasMaxLength(256);
        builder.Property(u=>u.Role).HasMaxLength(256);
    }
}