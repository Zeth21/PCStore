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
        //List<Coupon> CouponGenerator();
        //List<CouponUsage> CouponUsageGenerator();
        //List<Discount> DiscountGenerator();
        //List<DiscountRate> DiscountRateGenerator();

        List<Notification> NotificationGenerator(int count, List<User> users);
        List<Order> OrderGenerator(int count, List<User> users, List<Address> addresses);
        //List<OrderProductList> OrderProductListGenerator();
        //List<OrderStatus> OrderStatusGenerator();
        List<Product> ProductGenerator(int count, List<Brand> brands, List<Category> categories, List<ProductType> productTypes);
        List<ProductPhoto> ProductPhotoGenerator(List<Product> products);
        List<ProductRate> ProductRateGenerator(List<Product> products, List<User> users);

        //List<StatusName> StatusNameGenerator();
        List<User> UserGenerator(int count);

        //ATTRIBUTES
        List<ProductType> TypeGenerator();
        List<AttributeDefinition> AttributeDefinitionGenerator();
        List<ProductTypeAttribute> ProductTypeAttributeGenerator();
        List<ProductAttribute> ProductAttributeGenerator(List<Product> products, List<ProductTypeAttribute> productTypeAttributes, List<AttributeDefinition> attributeDefinitions);

        string GenerateAttributeValue(AttributeDefinition attributeDefinition);
    }
}
