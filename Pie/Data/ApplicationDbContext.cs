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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            //Database.EnsureCreated();
        }

        public DbSet<BaseDoc> BaseDocs { get; set; }

        public DbSet<DocIn> DocsIn { get; set; }
        public DbSet<DocInBaseDoc> DocInBaseDocs { get; set; }
        public DbSet<DocInProduct> DocInProducts { get; set; }

        public DbSet<DocOut> DocsOut { get; set; }
        public DbSet<DocOutBaseDoc> DocOutBaseDocs { get; set; }
        public DbSet<DocOutProduct> DocOutProducts { get; set; }

        //public DbSet<Queue> Queues { get; set; }
        public DbSet<QueueIn> QueuesIn { get; set; }
        public DbSet<QueueOut> QueuesOut { get; set; }

        //public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusIn> StatusesIn { get; set; }
        public DbSet<StatusOut> StatusesOut { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DocIn>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null).HasKey(d => d.Id);
            builder.Entity<DocIn>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocInBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.DocIn).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocInId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocInProduct>().HasKey(p => p.Id);
            builder.Entity<DocInProduct>().HasOne(p => p.DocIn).WithMany(d => d.Products).HasForeignKey(p => p.DocInId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<DocOut>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null).HasKey(d => d.Id);
            builder.Entity<DocOut>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocOutBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.DocOut).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocOutId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocOutProduct>().HasKey(p => p.Id);
            builder.Entity<DocOutProduct>().HasOne(p => p.DocOut).WithMany(d => d.Products).HasForeignKey(p => p.DocOutId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StatusIn>().HasQueryFilter(d => d.Active).HasKey(s => s.Id);
            builder.Entity<StatusOut>().HasQueryFilter(d => d.Active).HasKey(s => s.Id);

            builder.Entity<QueueIn>().HasQueryFilter(d => d.Active).HasKey(q => q.Id);
            builder.Entity<QueueOut>().HasQueryFilter(d => d.Active).HasKey(q => q.Id);

            builder.Entity<Product>().HasQueryFilter(d => d.Active).HasKey(p => p.Id);
            builder.Entity<Warehouse>().HasQueryFilter(d => d.Active).HasKey(w => w.Id);

            InitialData.Seed(builder);
        }
    }
}