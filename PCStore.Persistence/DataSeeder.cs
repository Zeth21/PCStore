using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCStore.Domain.Entities;
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
            var checkData = await _projectDbContext.Products.FirstOrDefaultAsync();
            if (checkData is not null)
            {
                return;
            }
            var brands = _faker.BrandGenerator(20);
            _projectDbContext.Brands.AddRange(brands);
            await _projectDbContext.SaveChangesAsync();

            var categories = _faker.CategoryGenerator(2, 10);
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




        }

    }
}
