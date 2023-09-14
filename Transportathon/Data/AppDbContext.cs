using Microsoft.EntityFrameworkCore;
using Transportathon.Models;

namespace Transportathon.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CreateJob> CreateJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CreateJob>()
                .HasKey(c => c.CreatedJobId);

        }

        }
    }
