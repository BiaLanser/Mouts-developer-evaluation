using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //PRODUCT
        modelBuilder.Entity<Product>(entity =>
        {
           entity.OwnsOne(p => p.Rating, r =>
            {
                r.Property(rating => rating.Rate)
                    .HasColumnType("decimal(18, 2)");
            });

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        });

        //CART 
        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(cp => cp.Cart)
            .WithMany(c => c.Products)
            .HasForeignKey(cp => cp.CartId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_CartProduct_Cart_CartId");

            entity.HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_CartProduct_Product_ProductId");
        });

        //SALE
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasOne(s => s.Cart)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.Items)
                  .WithOne(si => si.Sale)
                  .HasForeignKey(si => si.SaleId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(s => s.Discount)
                .HasColumnType("decimal(18, 2)");

            entity.Property(s => s.TotalSaleAmount)
                .HasColumnType("decimal(18, 2)");
        });

        // SALEITEM
        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasOne(si => si.Product)
                  .WithMany()
                  .HasForeignKey(si => si.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(si => si.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            entity.Property(si => si.UnitPrice)
                .HasColumnType("decimal(18, 2)");
        });

        //USER

        modelBuilder.Entity<User>(entity =>
        { 
            entity.Property(u => u.Role)
                  .HasConversion<string>();
            entity.Property(u => u.Status)
                  .HasConversion<string>();
        });


        base.OnModelCreating(modelBuilder);
    }

}

public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}