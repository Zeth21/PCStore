using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCStore.Domain.Entities;
using PCStore.Persistence.Migrations;


namespace PCStore.Persistence.Context
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZEYIT;initial Catalog=PCStore;trust server certificate=true;integrated security=true");
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponUsage> CouponUsages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProductList> OrderProductLists { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<StatusName> StatusNames { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FollowedProduct> FollowedProducts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        public DbSet<ProductTypeAttribute> ProductTypeAttributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<CouponBrand> CouponBrands { get; set; }
        public DbSet<CouponCategory> CouponCategories { get; set; }
        public DbSet<CouponProduct> CouponProducts { get; set; }
        public DbSet<CouponProductType> CouponProductTypes { get; set; }
        public DbSet<DiscountUsage> DiscountUsages { get; set; }
        public DbSet<DiscountProduct> DiscountProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //KUPON AYARLARI
            modelBuilder.Entity<Coupon>()
                .HasIndex(c => c.CouponCode)
                .IsUnique();

        }
    }
}
