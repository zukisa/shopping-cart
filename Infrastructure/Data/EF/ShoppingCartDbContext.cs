using Core.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EF
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(ConfigureUser);
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<ProductOrder>(ConfigureProductOrder);
            builder.Entity<Order>(ConfigureOrder);
        }
        private static void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.SetDefaultConstraintForId();

            builder.Property(_ => _.Email).IsRequired().HasColumnType("VARCHAR(200)");
            builder.Property(_ => _.Password).IsRequired().HasColumnType("VARCHAR(200)");
            builder.HasKey(_ => _.Id);
            
        }
        private static void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.SetDefaultConstraintForId();
            builder.Property(_ => _.Name).IsRequired().HasColumnType("VARCHAR(100)");
            builder.Property(_ => _.Description).IsRequired().HasColumnType("VARCHAR(200)");
            builder.Property(_ => _.Price).IsRequired().HasColumnType("DECIMAL(9,2)");
            builder.Property(_ => _.Image).IsRequired().HasColumnType("VARCHAR(100)");
            builder.Property(_ => _.StockLevel).IsRequired().HasColumnType("INT");
        }
        private static void ConfigureProductOrder(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable(nameof(ProductOrder));

            builder.SetDefaultConstraintForId();
            builder.Property(_ => _.Quantity).IsRequired().HasColumnType("INT");

            builder.Property(_ => _.ProductId).IsRequired();
            builder.HasOne(_ => _.Product).WithMany().HasForeignKey(_ => _.ProductId).HasConstraintName("FK_ProductOrder_ProductId").OnDelete(DeleteBehavior.Restrict);

            builder.Property(_ => _.OrderId).IsRequired();
            builder.HasOne(_ => _.Order).WithMany().HasForeignKey(_ => _.OrderId).HasConstraintName("FK_ProductOrder_OrderId").OnDelete(DeleteBehavior.Restrict);
        }
        private static void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).HasColumnType("VARCHAR(50)");
            
            builder.Property(_ => _.DateCreated).IsRequired().HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
            
            builder.Property(_ => _.UserId).IsRequired();
            builder.HasOne(_ => _.User).WithMany().HasForeignKey(_ => _.UserId).HasConstraintName("FK_Order_UserId").OnDelete(DeleteBehavior.Restrict);

        }
    }
}
