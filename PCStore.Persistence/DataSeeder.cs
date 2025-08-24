using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCStore.Domain.Entities;
using PCStore.Domain.Enum;
using PCStore.Domain.IdentityFaker;
using PCStore.Persistence.Context;

namespace PCStore.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ProjectDbContext _projectDbContext;
        private readonly IFakerGenerator _faker;
        private readonly UserManager<User> _userManager;

        public DataSeeder(ProjectDbContext projectDbContext, IFakerGenerator faker, UserManager<User> userManager)
        {
            _projectDbContext = projectDbContext;
            _faker = faker;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            using (var transaction = await _projectDbContext.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var checkData = await _projectDbContext.Products.FirstOrDefaultAsync();
                    if (checkData is not null)
                    {
                        return;
                    }
                    var brands = _faker.BrandGenerator(20);
                    _projectDbContext.Brands.AddRange(brands);
                    await _projectDbContext.SaveChangesAsync();

                    var categories = _faker.CategoryGenerator(30);
                    _projectDbContext.Categories.AddRange(categories);
                    await _projectDbContext.SaveChangesAsync();


                    //Product
                    var types = _faker.TypeGenerator();
                    _projectDbContext.AddRange(types);
                    await _projectDbContext.SaveChangesAsync();

                    var products = _faker.ProductGenerator(200, brands, categories, types);
                    _projectDbContext.Products.AddRange(products);
                    await _projectDbContext.SaveChangesAsync();

                    var attributeDefinitons = _faker.AttributeDefinitionGenerator();
                    _projectDbContext.AttributeDefinitions.AddRange(attributeDefinitons);
                    await _projectDbContext.SaveChangesAsync();

                    var productTypeAttributes = _faker.ProductTypeAttributeGenerator();
                    _projectDbContext.ProductTypeAttributes.AddRange(productTypeAttributes);
                    await _projectDbContext.SaveChangesAsync();

                    var productAttributes = _faker.ProductAttributeGenerator(products, productTypeAttributes, attributeDefinitons);
                    _projectDbContext.ProductAttributes.AddRange(productAttributes);
                    await _projectDbContext.SaveChangesAsync();

                    var productphotos = _faker.ProductPhotoGenerator(products);
                    _projectDbContext.ProductPhotos.AddRange(productphotos);
                    await _projectDbContext.SaveChangesAsync();

                    var users = _faker.UserGenerator(20);
                    foreach (var user in users)
                    {
                        await _userManager.CreateAsync(user, "Test1234!");
                    }

                    var addresses = _faker.AddressGenerator(100, users);
                    _projectDbContext.Addresses.AddRange(addresses);
                    await _projectDbContext.SaveChangesAsync();

                    var notifications = _faker.NotificationGenerator(100, users);
                    _projectDbContext.Notifications.AddRange(notifications);
                    await _projectDbContext.SaveChangesAsync();

                    var productrates = _faker.ProductRateGenerator(products, users);
                    _projectDbContext.ProductRates.AddRange(productrates);
                    await _projectDbContext.SaveChangesAsync();

                    var orders = _faker.OrderGenerator(100, users, addresses);
                    _projectDbContext.Orders.AddRange(orders);
                    await _projectDbContext.SaveChangesAsync();

                    var comments = _faker.CommentGenerator(200, products, users);
                    _projectDbContext.Comments.AddRange(comments);
                    await _projectDbContext.SaveChangesAsync();

                    var commentvotes = _faker.CommentVoteGenerator(200, comments, users);
                    _projectDbContext.CommentVotes.AddRange(commentvotes);
                    await _projectDbContext.SaveChangesAsync();

                    var answers = _faker.AnswerGenerator(1000, comments, users);
                    _projectDbContext.Answers.AddRange(answers);
                    await _projectDbContext.SaveChangesAsync();

                    var answervotes = _faker.AnswerVoteGenerator(200, answers, users);
                    _projectDbContext.AnswerVotes.AddRange(answervotes);
                    await _projectDbContext.SaveChangesAsync();

                    //EKLENENLER

                    var coupons = _faker.CouponGenerator(25);
                    _projectDbContext.Coupons.AddRange(coupons);
                    await _projectDbContext.SaveChangesAsync();

                    var discounts = _faker.DiscountGenerator(15);
                    _projectDbContext.Discounts.AddRange(discounts);
                    await _projectDbContext.SaveChangesAsync();

                    var productCouponIds = coupons.Where(x => x.CouponTargetType == CouponTargetType.SpecificProducts).Select(x => x.CouponId).ToList();
                    var productIds = products.Select(x => x.ProductId).ToList();
                    var couponProducts = _faker.CouponProductGenerator(productCouponIds, productIds);
                    _projectDbContext.CouponProducts.AddRange(couponProducts);
                    await _projectDbContext.SaveChangesAsync();

                    var brandCouponIds = coupons.Where(x => x.CouponTargetType == CouponTargetType.Brands).Select(x => x.CouponId).ToList();
                    var brandIds = brands.Select(x => x.BrandId).ToList();
                    var couponBrands = _faker.CouponBrandGenerator(brandCouponIds, brandIds);
                    _projectDbContext.CouponBrands.AddRange(couponBrands);
                    await _projectDbContext.SaveChangesAsync();

                    var categoryCouponIds = coupons.Where(x => x.CouponTargetType == CouponTargetType.Categories).Select(x => x.CouponId).ToList();
                    var categoryIds = categories.Select(x => x.CategoryId).ToList();
                    var couponCategories = _faker.CouponCategoriesGenerator(categoryCouponIds, categoryIds);
                    _projectDbContext.CouponCategories.AddRange(couponCategories);
                    await _projectDbContext.SaveChangesAsync();

                    var typeCouponIds = coupons.Where(x => x.CouponTargetType == CouponTargetType.ProductTypes).Select(x => x.CouponId).ToList();
                    var typeIds = types.Select(x => x.Id).ToList();
                    var couponTypes = _faker.CouponProductTypeGenerator(typeCouponIds, typeIds);
                    _projectDbContext.CouponProductTypes.AddRange(couponTypes);
                    await _projectDbContext.SaveChangesAsync();

                    var discountIds = discounts.Select(x => x.DiscountId).ToList();
                    var discountProducts = _faker.DiscountProductGenerator(discountIds, productIds);
                    _projectDbContext.DiscountProducts.AddRange(discountProducts);
                    await _projectDbContext.SaveChangesAsync();

                    var userIds = users.Select(x => x.Id).ToList();
                    var followedProducts = _faker.FollowedProductGenerator(userIds, productIds);
                    _projectDbContext.FollowedProducts.AddRange(followedProducts);
                    await _projectDbContext.SaveChangesAsync();

                    var shopCartItems = _faker.ShoppingCartItemGenerator(userIds, productIds);
                    _projectDbContext.ShoppingCartItems.AddRange(shopCartItems);
                    await _projectDbContext.SaveChangesAsync();

                    var statusNames = _faker.StatusNameGenerator();
                    _projectDbContext.StatusNames.AddRange(statusNames);
                    await _projectDbContext.SaveChangesAsync();

                    var orderStatus = _faker.OrderStatusGenerator(orders);
                    _projectDbContext.OrderStatuses.AddRange(orderStatus);
                    await _projectDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex) 
                {
                    await transaction.RollbackAsync();
                    throw new ApplicationException("Seeder error! :" + ex.ToString());
                }
            }
        }

    }
}
