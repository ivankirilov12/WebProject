using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Models.Models;

namespace PcPartPicker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<StorageOption> StorageOptions { get; set; }
        public DbSet<MemoryOption> MemoryOptions { get; set; }
        public DbSet<SystemBuild> SystemBuilds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //defining part-build relations
            builder.Entity<SystemBuild>().HasOne(x => x.Cpu).WithMany(s => s.SystemBuilds);
            builder.Entity<SystemBuild>().HasOne(x => x.Gpu).WithMany(s => s.SystemBuilds);
            builder.Entity<SystemBuild>().HasOne(x => x.Case).WithMany(s => s.SystemBuilds);
            builder.Entity<SystemBuild>().HasOne(x => x.Motherboard).WithMany(s => s.SystemBuilds);
            builder.Entity<SystemBuild>().HasOne(x => x.MemoryOption).WithMany(s => s.SystemBuilds);
            builder.Entity<SystemBuild>().HasOne(x => x.StorageOption).WithMany(s => s.SystemBuilds);


            base.OnModelCreating(builder);
        }        
    }
}
