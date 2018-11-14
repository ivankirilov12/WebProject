using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<Motherboard> Motherboard { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<SystemBuild> SystemBuild { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //defining part-build relations
            builder.Entity<SystemBuild>().HasOne(x => x.Gpu).WithOne(x => x.SystemBuild).HasForeignKey<Gpu>(b => b.SystemBuildId);
            builder.Entity<SystemBuild>().HasOne(x => x.Cpu).WithOne(x => x.SystemBuild).HasForeignKey<Cpu>(b => b.SystemBuildId);
            builder.Entity<SystemBuild>().HasOne(x => x.Case).WithOne(x => x.SystemBuild).HasForeignKey<Case>(b => b.SystemBuildId);
            builder.Entity<SystemBuild>().HasOne(x => x.Storage).WithOne(x => x.SystemBuild).HasForeignKey<Storage>(b => b.SystemBuildId);
            builder.Entity<SystemBuild>().HasOne(x => x.Ram).WithOne(x => x.SystemBuild).HasForeignKey<Ram>(b => b.SystemBuildId);
            builder.Entity<SystemBuild>().HasOne(x => x.Motherboard).WithOne(x => x.SystemBuild).HasForeignKey<Motherboard>(b => b.SystemBuildId);

            //define identity primary keys
            builder.Entity<Cpu>().Property(p => p.CpuId).ValueGeneratedOnAdd();
            builder.Entity<Gpu>().Property(p => p.GpuId).ValueGeneratedOnAdd();
            builder.Entity<Case>().Property(p => p.CaseId).ValueGeneratedOnAdd();
            builder.Entity<Motherboard>().Property(p => p.MotherboardId).ValueGeneratedOnAdd();
            builder.Entity<Ram>().Property(p => p.RamId).ValueGeneratedOnAdd();
            builder.Entity<Storage>().Property(p => p.StorageId).ValueGeneratedOnAdd();

            builder.ForSqlServerUseIdentityColumns();
            base.OnModelCreating(builder);
        }        
    }
}
