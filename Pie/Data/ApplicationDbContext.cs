using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;
using Pie.Data.Models.Identity;
using Pie.Data.Models.In;
using Pie.Data.Models.Out;

namespace Pie.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<BaseDoc> BaseDocs => Set<BaseDoc>();
        public DbSet<ChangeReason> ChangeReasons => Set<ChangeReason>();
        public DbSet<DeliveryArea> DeliveryAreas => Set<DeliveryArea>();
        public DbSet<Partner> Partners => Set<Partner>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();

        // In
        public DbSet<ChangeReasonIn> ChangeReasonsIn => Set<ChangeReasonIn>();
        public DbSet<DocIn> DocsIn => Set<DocIn>();
        public DbSet<DocInBaseDoc> DocInBaseDocs => Set<DocInBaseDoc>();
        public DbSet<DocInHistory> DocsInHistory => Set<DocInHistory>();
        public DbSet<DocInProduct> DocInProducts => Set<DocInProduct>();
        public DbSet<DocInProductHistory> DocInProductsHistory => Set<DocInProductHistory>();
        public DbSet<QueueIn> QueuesIn => Set<QueueIn>();
        public DbSet<StatusIn> StatusesIn => Set<StatusIn>();

        // Out
        public DbSet<ChangeReasonOut> ChangeReasonsOut => Set<ChangeReasonOut>();
        public DbSet<DocOut> DocsOut => Set<DocOut>();
        public DbSet<DocOutBaseDoc> DocOutBaseDocs => Set<DocOutBaseDoc>();
        public DbSet<DocOutHistory> DocsOutHistory => Set<DocOutHistory>();
        public DbSet<DocOutProduct> DocOutProducts => Set<DocOutProduct>();
        public DbSet<DocOutProductHistory> DocOutProductsHistory => Set<DocOutProductHistory>();
        public DbSet<QueueOut> QueuesOut => Set<QueueOut>();
        public DbSet<StatusOut> StatusesOut => Set<StatusOut>();
        public DbSet<QueueNumber> QueueNumber => Set<QueueNumber>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BaseDoc>().HasKey(e => e.Id);
            builder.Entity<ChangeReason>().HasKey(c => c.Id);
            builder.Entity<DeliveryArea>().HasKey(c => c.Id);
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Partner>().HasKey(p => p.Id);
            builder.Entity<Warehouse>().HasKey(w => w.Id);

            // In
            builder.Entity<DocIn>().HasKey(d => d.Id);
            builder.Entity<DocIn>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Partner).WithMany().HasForeignKey(d => d.PartnerId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<DocIn>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null);

            builder.Entity<DocInBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.Doc).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocInHistory>().HasKey(h => h.Id);
            //builder.Entity<DocInHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInHistory>().HasOne(h => h.Status).WithMany().HasForeignKey(h => h.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocInHistory>().HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocInProduct>().HasKey(p => p.Id);
            builder.Entity<DocInProduct>().HasOne(p => p.Doc).WithMany(d => d.Products).HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocInProductHistory>().HasKey(h => h.Id);
            //builder.Entity<DocInProductHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInProductHistory>().HasOne(h => h.Product).WithMany().HasForeignKey(h => h.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInProductHistory>().HasOne(h => h.ChangeReason).WithMany().HasForeignKey(h => h.ChangeReasonId).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocInProductHistory>().HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<QueueIn>().HasKey(q => q.Id);
            builder.Entity<StatusIn>().HasKey(s => s.Id);

            // Out
            builder.Entity<DocOut>().HasKey(d => d.Id);
            builder.Entity<DocOut>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOut>().HasOne(d => d.Partner).WithMany().HasForeignKey(d => d.PartnerId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<DocOut>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null);

            builder.Entity<DocOutBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.Doc).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocOutHistory>().HasKey(h => h.Id);
            //builder.Entity<DocOutHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<DocOutHistory>().HasOne(h => h.Status).WithMany().HasForeignKey(h => h.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOutHistory>().HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocOutProduct>().HasKey(p => p.Id);
            builder.Entity<DocOutProduct>().HasOne(p => p.Doc).WithMany(d => d.Products).HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocOutProductHistory>().HasKey(h => h.Id);
            //builder.Entity<DocOutProductHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<DocOutProductHistory>().HasOne(h => h.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutProductHistory>().HasOne(h => h.ChangeReason).WithMany().HasForeignKey(p => p.ChangeReasonId).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOutProductHistory>().HasOne(h => h.User).WithMany().HasForeignKey(p => p.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<QueueOut>().HasKey(q => q.Id);
            builder.Entity<StatusOut>().HasKey(s => s.Id);

            builder.Entity<QueueNumber>().HasKey(e => e.Value);

            DataSeed.Initialize(builder);
        }
    }
}