﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        }

        public DbSet<BaseDoc> BaseDocs { get; set; }
        public DbSet<ChangeReason> ChangeReasons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        // In
        public DbSet<ChangeReasonIn> ChangeReasonsIn { get; set; }
        public DbSet<DocIn> DocsIn { get; set; }
        public DbSet<DocInBaseDoc> DocInBaseDocs { get; set; }
        public DbSet<DocInHistory> DocsInHistory { get; set; }
        public DbSet<DocInProduct> DocInProducts { get; set; }
        public DbSet<DocInProductHistory> DocInProductsHistory { get; set; }
        public DbSet<QueueIn> QueuesIn { get; set; }
        public DbSet<StatusIn> StatusesIn { get; set; }

        // Out
        public DbSet<ChangeReasonOut> ChangeReasonsOut { get; set; }
        public DbSet<DocOut> DocsOut { get; set; }
        public DbSet<DocOutBaseDoc> DocOutBaseDocs { get; set; }
        public DbSet<DocOutHistory> DocsOutHistory { get; set; }
        public DbSet<DocOutProduct> DocOutProducts { get; set; }
        public DbSet<DocOutProductHistory> DocOutProductsHistory { get; set; }
        public DbSet<QueueOut> QueuesOut { get; set; }
        public DbSet<StatusOut> StatusesOut { get; set; }
        public DbSet<QueueNumber> QueueNumber { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BaseDoc>().HasKey(e => e.Id);
            builder.Entity<ChangeReason>().HasKey(c => c.Id);
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Warehouse>().HasKey(w => w.Id);

            // In
            builder.Entity<DocIn>().HasKey(d => d.Id);
            builder.Entity<DocIn>().HasOne(d => d.Status).WithMany().HasForeignKey(d => d.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Queue).WithMany().HasForeignKey(d => d.QueueKey).HasPrincipalKey(q => q.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocIn>().HasOne(d => d.Warehouse).WithMany().HasForeignKey(d => d.WarehouseId).HasPrincipalKey(w => w.Id).OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<DocIn>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null);

            builder.Entity<DocInBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.Doc).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocInHistory>().HasKey(h => h.Id);
            builder.Entity<DocInHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInHistory>().HasOne(h => h.Status).WithMany().HasForeignKey(h => h.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocInHistory>().HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocInProduct>().HasKey(p => p.Id);
            builder.Entity<DocInProduct>().HasOne(p => p.Doc).WithMany(d => d.Products).HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocInProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocInProductHistory>().HasKey(h => h.Id);
            builder.Entity<DocInProductHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
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
            //builder.Entity<DocOut>().HasQueryFilter(d => d.Active && d.StatusKey != null && d.QueueKey != null);

            builder.Entity<DocOutBaseDoc>().HasKey(b => b.Id);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.Doc).WithMany(d => d.BaseDocs).HasForeignKey(b => b.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutBaseDoc>().HasOne(b => b.BaseDoc).WithMany().HasForeignKey(b => b.BaseDocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocOutHistory>().HasKey(h => h.Id);
            builder.Entity<DocOutHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(h => h.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutHistory>().HasOne(h => h.Status).WithMany().HasForeignKey(h => h.StatusKey).HasPrincipalKey(s => s.Key).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<DocOutHistory>().HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<DocOutProduct>().HasKey(p => p.Id);
            builder.Entity<DocOutProduct>().HasOne(p => p.Doc).WithMany(d => d.Products).HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocOutProduct>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DocOutProductHistory>().HasKey(h => h.Id);
            builder.Entity<DocOutProductHistory>().HasOne(h => h.Doc).WithMany().HasForeignKey(p => p.DocId).HasPrincipalKey(d => d.Id).OnDelete(DeleteBehavior.Cascade);
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