using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;
using Pie.Data.Models.Application;

namespace Pie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaseDoc> BaseDocs { get; set; }

        public DbSet<DocIn> DocsIn { get; set; }
        public DbSet<DocInBaseDoc> DocInBaseDocs { get; set; }
        public DbSet<DocInProduct> DocInProducts { get; set; }

        public DbSet<DocOut> DocsOut { get; set; }
        public DbSet<DocOutBaseDoc> DocOutBaseDocs { get; set; }
        public DbSet<DocOutProduct> DocOutProducts { get; set; }

        public DbSet<Queue> Queues { get; set; }
        public DbSet<QueueIn> QueuesIn { get; set; }
        public DbSet<QueueOut> QueuesOut { get; set; }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusIn> StatusesIn { get; set; }
        public DbSet<StatusOut> StatusesOut { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DocIn>().HasKey(d => d.Id);
            builder.Entity<DocIn>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocInBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.DocIn).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocInId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<DocOut>().HasKey(d => d.Id);
            builder.Entity<DocOut>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocOutBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.DocOut).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocOutId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Status>().HasKey(s => s.Id);
            builder.Entity<Queue>().HasKey(q => q.Id);

            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Warehouse>().HasKey(w => w.Id);
        }
    }
}