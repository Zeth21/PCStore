using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Abstractions.ContextInterface
{
    public interface IProjectDbContext
    {
        DbSet<User> Users { get; }
        DbSet<IdentityRole> Roles { get; }
        DbSet<Answer> Answers { get; }
        DbSet<AnswerVote> AnswerVotes { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Category> Categories { get; }
        DbSet<Comment> Comments { get; }
        DbSet<CommentVote> CommentVotes { get; }
        DbSet<Coupon> Coupons { get; }
        DbSet<CouponUsage> CouponUsages { get; }
        DbSet<Discount> Discounts { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderProductList> OrderProductLists { get; }
        DbSet<OrderStatus> OrderStatuses { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductPhoto> ProductPhotos { get; }
        DbSet<ProductRate> ProductRates { get; }
        DbSet<StatusName> StatusNames { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Address> Addresses { get; }
        DbSet<FollowedProduct> FollowedProducts { get; }
        DbSet<ProductType> ProductTypes { get; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; }
        DbSet<ProductTypeAttribute> ProductTypeAttributes { get; }
        DbSet<ProductAttribute> ProductAttributes { get; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; }
        DbSet<CouponBrand> CouponBrands { get; }
        DbSet<CouponCategory> CouponCategories { get; }
        DbSet<CouponProduct> CouponProducts { get; }
        DbSet<CouponProductType> CouponProductTypes { get; }
        DbSet<DiscountUsage> DiscountUsages { get; }
        DbSet<DiscountProduct> DiscountProducts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
