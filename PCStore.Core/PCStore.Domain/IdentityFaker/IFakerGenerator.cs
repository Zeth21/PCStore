using PCStore.Domain.Entities;

namespace PCStore.Domain.IdentityFaker
{
    public interface IFakerGenerator
    {
        List<Address> AddressGenerator(int count, List<User> users);
        List<Answer> AnswerGenerator(int count, List<Comment> comments, List<User> users);
        List<AnswerVote> AnswerVoteGenerator(int count, List<Answer> answers, List<User> users);
        List<Brand> BrandGenerator(int count);
        List<Category> CategoryGenerator(int rootCount, int subCount);
        List<Comment> CommentGenerator(int count, List<Product> products, List<User> users);
        List<CommentVote> CommentVoteGenerator(int count, List<Comment> comments, List<User> users);


        List<Notification> NotificationGenerator(int count, List<User> users);
        List<Order> OrderGenerator(int count, List<User> users, List<Address> addresses);

        List<Product> ProductGenerator(int count, List<Brand> brands, List<Category> categories, List<ProductType> productTypes);
        List<ProductPhoto> ProductPhotoGenerator(List<Product> products);
        List<ProductRate> ProductRateGenerator(List<Product> products, List<User> users);

        List<User> UserGenerator(int count);

        //ATTRIBUTES
        List<ProductType> TypeGenerator();
        List<AttributeDefinition> AttributeDefinitionGenerator();
        List<ProductTypeAttribute> ProductTypeAttributeGenerator();
        List<ProductAttribute> ProductAttributeGenerator(List<Product> products, List<ProductTypeAttribute> productTypeAttributes, List<AttributeDefinition> attributeDefinitions);

        string GenerateAttributeValue(AttributeDefinition attributeDefinition);

        //SEEDER'A EKLENECEKLER
        List<Coupon> CouponGenerator(int count);
        List<Discount> DiscountGenerator(int count);
        List<CouponProduct> CouponProductGenerator(List<int> CouponIds, List<int> ProductIds);
        List<CouponBrand> CouponBrandGenerator(List<int> CouponIds, List<int> BrandIds);
        List<CouponCategory> CouponCategoriesGenerator(List<int> CouponIds, List<int> CategoryIds);
        List<CouponProductType> CouponProductTypeGenerator(List<int> CouponIds, List<int> TypeIds);
        List<DiscountProduct> DiscountProductGenerator(List<int> DiscountIds, List<int> ProductIds);
        List<FollowedProduct> FollowedProductGenerator(List<string> UserIds, List<int> ProductIds);
        List<ShoppingCartItem> ShoppingCartItemGenerator(List<string> UserIds, List<int> ProductIds);
        List<StatusName> StatusNameGenerator();
        List<OrderStatus> OrderStatusGenerator(List<Order> Orders);


        // Eksik generatorlar
        //List<CouponUsage> CouponUsageGenerator(int count, List<Coupon> coupons, List<User> users);
        //List<OrderProductList> OrderProductListGenerator(int count, List<Order> orders, List<Product> products);
        //List<DiscountUsage> DiscountUsageGenerator(int count, List<Discount> discounts, List<User> users);

    }
}
