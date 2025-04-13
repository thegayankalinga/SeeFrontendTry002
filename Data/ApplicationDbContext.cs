using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeeFrontendTry002.Models;

namespace SeeFrontendTry002.Data;

public class ApplicationDbContext: IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<FeatureData> FeatureData { get; set; }
    public DbSet<PredictionResult> PredictionResults { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AppUser>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(450);
        });
        
        builder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.ProjectManagerId)
            .OnDelete(DeleteBehavior.Restrict); // optional, depending on cascade behavior
        
        builder.Entity<Project>()
            .HasOne(p => p.FeatureData)
            .WithOne(fd => fd.Project)
            .HasForeignKey<FeatureData>(fd => fd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}