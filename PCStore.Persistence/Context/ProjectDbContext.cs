using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PCStore.Domain.Entities;


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

            //ADDRESS AYARLARI
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(a => a.Description)
                .IsUnique();

                entity.HasIndex(a => new { a.UserId, a.AddressName })
                .IsUnique();
            });

            //ANSWER AYARLARI
            modelBuilder.Entity<Answer>(entity => 
            {
                entity.Property(a => a.AnswerText)
                .HasMaxLength(200);
            });

            //ANSWERVOTE AYARLARI
            modelBuilder.Entity<AnswerVote>(entity => 
            {
                entity.HasIndex(a => new {a.AnswerVoteUserId, a.AnswerVoteAnswerId})
                .IsUnique();
            });

            //ATTRİBUTEDEFİNİTİON AYARLARI
            modelBuilder.Entity<AttributeDefinition>(entity => 
            {
                entity.HasIndex(ad => ad.Name)
                .IsUnique();

                entity.Property(ad => ad.Name)
                .HasMaxLength(50);
            });

            //BRAND AYARLARI

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(b => b.BrandName)
                .IsUnique();
            });


            //CATEGORY AYARLARI

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(b => b.CategoryName)
                .IsUnique();
            });

            //COMMENT AYARLARI

            modelBuilder.Entity<Comment>(entity => 
            {
                entity.Property(c => c.CommentText)
                .HasMaxLength(200);
            });

            //COMMENTVOTE AYARLARI

            modelBuilder.Entity<CommentVote>(entity =>
            {
                entity.HasIndex(a => new { a.CommentVoteUserId, a.CommentVoteCommentId })
                .IsUnique();
            });

            //KUPON AYARLARI
            modelBuilder.Entity<Coupon>(entity => 
            {
                entity.HasIndex(c => c.CouponCode)
                .IsUnique();

                entity.Property(c => c.Description)
                .HasMaxLength(200);

            });

            //COUPONBRAND AYARLARI
            modelBuilder.Entity<CouponBrand>(entity => 
            {
                entity.HasIndex(cb => new { cb.CouponId, cb.BrandId })
                .IsUnique();

            });
            //COUPONCATEGORY AYARLARI
            modelBuilder.Entity<CouponCategory>(entity => 
            {
                entity.HasIndex(cc => new { cc.CouponId, cc.CategoryId })
                .IsUnique();
            });
            //COUPONPRODUCT AYARLARI
            modelBuilder.Entity<CouponProduct>(entity => 
            {
                entity.HasIndex(cp => new { cp.CouponId, cp.ProductId })
                .IsUnique();
            });
            //COUPONPRODUCTTYPE AYARLARI
            modelBuilder.Entity<CouponProductType>(entity => 
            {
                entity.HasIndex(cpt => new { cpt.CouponId, cpt.ProductTypeId })
                .IsUnique();
            });

            //COUPONUSAGE AYARLARI
            modelBuilder.Entity<CouponUsage>(entity => 
            {
                entity.HasIndex(cu => new { cu.CouponUsageOrderId, cu.CouponUsageCouponId })
                .IsUnique();
            });

            //DİSCOUNT AYARLARI
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasIndex(d => d.DiscountName)
                .IsUnique();
            });

            //DİSCOUNTPRODUCT AYARLARI
            modelBuilder.Entity<DiscountProduct>(entity => 
            {
                entity.HasIndex(dp => new { dp.DiscountId, dp.ProductId })
                .IsUnique();
            });

            //DİSCOUNTUSAGE AYARLARI
            modelBuilder.Entity<DiscountUsage>(entity => 
            {
                entity.HasIndex(dp => new { dp.OrderId, dp.DiscountId })
                .IsUnique();
            });

            //FOLLOWEDPRODUCTS AYARLARI
            modelBuilder.Entity<FollowedProduct>(entity =>
            {
                entity.HasIndex(fp => new { fp.UserId, fp.ProductId})
                .IsUnique();
            });

            //ORDER AYARLARI
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderTotalCost)
                .HasColumnType("decimal(18,2)");
            });

            //ORDERPRODUCTLİST AYARLARI
            modelBuilder.Entity<OrderProductList>(entity => 
            {
                entity.Property(e => e.ProductPrice)
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductOldPrice)
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductOldTotalCost)
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductTotalCost)
                .HasColumnType("decimal(18,2)");
            });

            //ORDERSTATUS AYARLARI
            modelBuilder.Entity<OrderStatus>(entity => 
            {
                entity.HasIndex(os => new { os.OrderId, os.StatusId })
                .IsUnique();
            });

            //PRODUCT AYARLARI
            modelBuilder.Entity<Product>(entity => 
            {
                entity.HasIndex(p => p.ProductName)
                .IsUnique();

                entity.Property(p => p.ProductName)
                .HasMaxLength(100);

                entity.Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");
            });

            //PRODUCTATTRIBUTE AYARLARI
            modelBuilder.Entity<ProductAttribute>(entity => 
            {
                entity.HasIndex(pa => new { pa.AttributeDefinitionId, pa.ProductId })
                .IsUnique();
            });

            //PRODUCTPHOTO AYARLARI
            modelBuilder.Entity<ProductRate>(entity => 
            {
                entity.HasIndex(pp => new 
                {
                    pp.ProductRateUserId, pp.ProductRateProductId
                })
                .IsUnique();
            });

            //PRODUCTTYPE AYARLARI
            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasIndex(pt => pt.Name)
                .IsUnique();

                entity.Property(pt => pt.Name)
                .HasMaxLength(50);
            });

            //PRODUCTTYPEATTRİBUTE AYARLARI
            modelBuilder.Entity<ProductTypeAttribute>(entity => 
            {
                entity.HasIndex(pta => new { pta.AttributeDefinitionId, pta.ProductTypeId })
                .IsUnique();
            });

            //SHOPPİNGCARTİTEM AYARLARI
            modelBuilder.Entity<ShoppingCartItem>(entity => 
            {
                entity.HasIndex(sci => new { sci.ProductId, sci.UserId })
                .IsUnique();
            });

            //STATUSNAME AYARLARI
            modelBuilder.Entity<StatusName>(entity => 
            {
                entity.HasIndex(st => st.StatusNameString)
                .IsUnique();
            });

            //ORDERSTATUS AYARLARI
            modelBuilder.Entity<OrderStatus>(entity => 
            {
                entity.HasIndex(e => new { e.OrderId, e.StatusNameId })
                .IsUnique();
            });

        }
    }
}
