using Bogus;
using PCStore.Domain.Entities;
using PCStore.Domain.Enum;
using System.Linq;
using System.Runtime.Serialization;

namespace PCStore.Domain.IdentityFaker
{
    public class FakerGenerator : IFakerGenerator
    {

        List<Address> IFakerGenerator.AddressGenerator(int count, List<User> users)
        {
            var result = new List<Address>();
            var addressFaker = new Faker<Address>()
                .RuleFor(a => a.UserId, f => f.PickRandom(users).Id)
                .RuleFor(a => a.AddressName, f => f.Address.StreetName())
                .RuleFor(a => a.Description, f => f.Address.FullAddress());
            for (int i = 0; i < count; i++)
            {
                var address = addressFaker.Generate();
                result.Add(address);
            }
            return result;
        }

        List<Answer> IFakerGenerator.AnswerGenerator(int count, List<Comment> comments, List<User> users)
        {
            var result = new List<Answer>();
            var answerFaker = new Faker<Answer>()
                    .RuleFor(a => a.AnswerText, f => f.Lorem.Sentence(5))
                    .RuleFor(a => a.AnswerUserId, f => f.PickRandom(users).Id)
                    .RuleFor(a => a.AnswerCommentId, f => f.PickRandom(comments).CommentId)
                    .RuleFor(a => a.AnswerUpVoteCount, f => f.Random.Int(0, 100))
                    .RuleFor(a => a.AnswerDownVoteCount, f => f.Random.Int(0, 100));
            for (int i = 0; i < count; i++)
            {
                var answer = answerFaker.Generate();
                result.Add(answer);
            }
            return result;
        }

        List<AnswerVote> IFakerGenerator.AnswerVoteGenerator(int count, List<Answer> answers, List<User> users)
        {
            var result = new List<AnswerVote>();

            var answerVoteFaker = new Faker<AnswerVote>()
                .RuleFor(a => a.AnswerVoteValue, f => f.PickRandom<VoteType>())
                .RuleFor(a => a.AnswerVoteUserId, f => f.PickRandom(users).Id)
                .RuleFor(a => a.AnswerVoteAnswerId, f => f.PickRandom(answers).AnswerId);

            for (int i = 0; i < count; i++)
            {
                var answerVote = answerVoteFaker.Generate();
                result.Add(answerVote);
            }
            return result;
        }

        List<Brand> IFakerGenerator.BrandGenerator(int count)
        {
            var result = new List<Brand>();
            var brandFaker = new Faker<Brand>()
                   .RuleFor(a => a.BrandName, f => f.Company.CompanyName());

            for (int i = 0; i < count; i++)
            {
                var brand = brandFaker.Generate();
                result.Add(brand);
            }
            return result;
        }

        List<Category> IFakerGenerator.CategoryGenerator(int rootCount, int subCount)
        {
            var result = new List<Category>();
            var faker = new Faker();

            for (int i = 0; i < rootCount; i++)
            {
                var rootCategory = new Category
                {
                    CategoryName = faker.Commerce.Categories(1)[0],
                    ParentCategoryId = null
                };
                result.Add(rootCategory);

                for (int j = 0; j < subCount; j++)
                {
                    var subCategory = new Category
                    {
                        CategoryName = faker.Commerce.Categories(1)[0],
                        // ParentCategoryId daha sonra atanacak (SaveChanges sonrası ID'lere ulaşınca)
                    };
                    result.Add(subCategory);
                }
            }

            return result;
        }


        List<Comment> IFakerGenerator.CommentGenerator(int count, List<Product> products, List<User> users)
        {
            var result = new List<Comment>();
            var commentFaker = new Faker<Comment>()
                .RuleFor(a => a.CommentText, f => f.Lorem.Sentence(10))
                .RuleFor(a => a.CommentIsQuestion, f => f.Random.Bool())
                .RuleFor(a => a.CommentUserId, f => f.PickRandom(users).Id)
                .RuleFor(a => a.CommentProductId, f => f.PickRandom(products).ProductId)
                .RuleFor(a => a.CommentAnswerCount, f => f.Random.Int(0, 150))
                .RuleFor(a => a.CommentUpVoteCount, f => f.Random.Int(0, 150))
                .RuleFor(a => a.CommentDownVoteCount, f => f.Random.Int(0, 150));
            for (int i = 0; i < count; i++)
            {
                var comment = commentFaker.Generate();
                result.Add(comment);
            }
            return result;
        }

        List<CommentVote> IFakerGenerator.CommentVoteGenerator(int count, List<Comment> comments, List<User> users)
        {
            var result = new List<CommentVote>();
            var commentVoteFaker = new Faker<CommentVote>()
                .RuleFor(a => a.CommentVoteValue, f => f.PickRandom<VoteType>())
                .RuleFor(a => a.CommentVoteUserId, f => f.PickRandom(users).Id)
                .RuleFor(a => a.CommentVoteCommentId, f => f.PickRandom(comments).CommentId);
            for (int i = 0; i < count; i++)
            {
                var commentVote = commentVoteFaker.Generate();
                result.Add(commentVote);
            }
            return result;
        }

        List<Notification> IFakerGenerator.NotificationGenerator(int count, List<User> users)
        {
            var result = new List<Notification>();
            var notificationFaker = new Faker<Notification>()
                .RuleFor(a => a.NotificationType, f => f.PickRandom<NotifType>())
                .RuleFor(a => a.NotificationTitle, f => f.Hacker.Phrase())
                .RuleFor(a => a.NotificationStatus, f => f.Random.Bool())
                .RuleFor(a => a.NotificationContent, f => f.Lorem.Paragraph(2))
                .RuleFor(a => a.NotificationUserId, f => f.PickRandom(users).Id);
            for (int i = 0; i < count; i++)
            {
                var notification = notificationFaker.Generate();
                result.Add(notification);
            }
            return result;
        }

        List<Order> IFakerGenerator.OrderGenerator(int count, List<User> users, List<Address> addresses)
        {
            var result = new List<Order>();
            var orderFaker = new Faker<Order>()
                .RuleFor(a => a.OrderTotalCost, f => 0)
                .RuleFor(a => a.OrderIsActive, f => f.Random.Bool())
                .RuleFor(a => a.OrderAddressId, f => f.PickRandom(addresses).Id)
                .RuleFor(a => a.OrderUserId, f => f.PickRandom(users).Id);
            for (int i = 0; i <= count - 1; i++)
            {
                var order = orderFaker.Generate();
                result.Add(order);
            }
            return result;
        }



        List<ProductPhoto> IFakerGenerator.ProductPhotoGenerator(List<Product> products)
        {
            var result = new List<ProductPhoto>();
            for (int i = 0; i <= products.Count - 2; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    var photo = new ProductPhoto
                    {
                        PhotoPath = i.ToString() + ".Photo" + j.ToString(),
                        PhotoName = i.ToString() + ".Photo" + j.ToString(),
                        PhotoProductId = products[i].ProductId

                    };
                    result.Add(photo);
                }
            }
            return result;
        }

        List<ProductRate> IFakerGenerator.ProductRateGenerator(List<Product> products, List<User> users)
        {
            var result = new List<ProductRate>();
            var rnd = new Random();
            var rateFaker = new Faker<ProductRate>()
                .RuleFor(a => a.ProductRateScore, f => f.Random.Decimal(0, 10))
                .RuleFor(a => a.ProductRateUserId, f => f.PickRandom(users).Id);
            for (int i = 0; i <= products.Count - 1; i++)
            {
                for (int j = 0; j <= rnd.Next(5, 20) - 1; j++)
                {
                    var rate = rateFaker.Generate();
                    rate.ProductRateProductId = products[i].ProductId;
                    result.Add(rate);
                }
            }
            return result;
        }


        List<User> IFakerGenerator.UserGenerator(int count)
        {
            var result = new List<User>();
            var userFaker = new Faker<User>()
                .RuleFor(a => a.Name, f => f.Person.UserName)
                .RuleFor(a => a.Surname, f => f.Person.UserName)
                .RuleFor(a => a.UserName, f => f.Person.UserName)
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber());

            //var customer = new User
            //{
            //    Name = "Customer",
            //    Surname = "Customer",
            //    Email = "customer@example.com",
            //    UserName = "Customer",
            //    EmailConfirmed = true
            //};
            //result.Add(customer);

            for (int i = 0; i < count; i++)
            {
                var user = userFaker.Generate();
                result.Add(user);
            }
            return result;
        }

        List<Product> IFakerGenerator.ProductGenerator(int count, List<Brand> brands, List<Category> categories, List<ProductType> productTypes)
        {
            var result = new List<Product>();
            var productFaker = new Faker<Product>()
                .RuleFor(a => a.ProductName, f => f.Commerce.ProductName())
                .RuleFor(a => a.ProductPrice, f => f.Random.Decimal(1500, 10000))
                .RuleFor(a => a.ProductStock, f => (short)f.Random.Int(0, 99))
                .RuleFor(a => a.ProductBrandId, f => f.PickRandom(brands).BrandId)
                .RuleFor(a => a.ProductCategoryId, f => f.PickRandom(categories).CategoryId)
                .RuleFor(a => a.ProductTotalRate, f => f.Random.Int(0, 99))
                .RuleFor(a => a.ProductRateScore, f => f.Random.Decimal(0, 10))
                .RuleFor(a => a.ProductTypeId, f => f.PickRandom(productTypes).Id);
            for (int i = 0; i < count; i++)
            {
                var product = productFaker.Generate();
                result.Add(product);
            }
            return result;
        }

        public List<ProductType> TypeGenerator()
        {
            var result = new List<ProductType>();
            List<string> types = [
                "CPU",
                "GPU",
                "Motherboard",
                "RAM",
                "SSD",
                "HDD",
                "PowerSupply",
                "Case",
                "CoolingSystem",
                "Monitor",
                "Keyboard",
                "Mouse",
                "Laptop"
            ];
            for (int i = 0; i < types.Count; i++)
            {
                var type = new ProductType { Name = types[i] };
                result.Add(type);
            }
            return result;
        }
        public List<AttributeDefinition> AttributeDefinitionGenerator()
        {
            var attributes = new List<AttributeDefinition>
{
    // Ortak özellikler
    new() { Name = "Description", DataType = "string", IsRequired = false },

    // CPU
    new() { Name = "CoreCount", DataType = "int", IsRequired = true },
    new() { Name = "ThreadCount", DataType = "int", IsRequired = true },
    new() { Name = "BaseClockGHz", DataType = "float", IsRequired = true, Unit = "GHz" },
    new() { Name = "BoostClockGHz", DataType = "float", IsRequired = false, Unit = "GHz" },
    new() { Name = "SocketType", DataType = "string", IsRequired = true },
    new() { Name = "TDP", DataType = "int", IsRequired = false, Unit = "W" },

    // GPU
    new() { Name = "Chipset", DataType = "string", IsRequired = true },
    new() { Name = "MemorySizeGB", DataType = "int", IsRequired = true, Unit = "GB" },
    new() { Name = "MemoryType", DataType = "string", IsRequired = true },
    new() { Name = "Interface", DataType = "string", IsRequired = true },
    new() { Name = "Watt", DataType = "int", IsRequired = false, Unit = "W" },

    // Motherboard
    new() { Name = "FormFactor", DataType = "string", IsRequired = true },
    new() { Name = "RAMType", DataType = "string", IsRequired = true },
    new() { Name = "MaxRAM", DataType = "int", IsRequired = true, Unit = "GB" },
    new() { Name = "PCIeSlots", DataType = "int", IsRequired = false },
    new() { Name = "SATAPorts", DataType = "int", IsRequired = false },

    // RAM
    new() { Name = "FrequencyMHz", DataType = "int", IsRequired = true, Unit = "MHz" },
    new() { Name = "Latency", DataType = "string", IsRequired = false },
    new() { Name = "Voltage", DataType = "float", IsRequired = false, Unit = "V" },
    new() { Name = "RGB", DataType = "bool", IsRequired = false },

    // SSD & HDD
    new() { Name = "CapacityGB", DataType = "int", IsRequired = true, Unit = "GB" },
    new() { Name = "ReadSpeedMBps", DataType = "int", IsRequired = false, Unit = "MB/s" },
    new() { Name = "WriteSpeedMBps", DataType = "int", IsRequired = false, Unit = "MB/s" },
    new() { Name = "DriveType", DataType = "string", IsRequired = true },

    // PowerSupply
    new() { Name = "EfficiencyRating", DataType = "string", IsRequired = false },

    // Case
    new() { Name = "CaseType", DataType = "string", IsRequired = true },
    new() { Name = "HasGlassPanel", DataType = "bool", IsRequired = false },

    // CoolingSystem
    new() { Name = "CoolingType", DataType = "string", IsRequired = true },
    new() { Name = "FanCount", DataType = "int", IsRequired = false },

    // Monitor
    new() { Name = "ScreenSizeInch", DataType = "float", IsRequired = true, Unit = "inch" },
    new() { Name = "Resolution", DataType = "string", IsRequired = true },
    new() { Name = "RefreshRateHz", DataType = "int", IsRequired = false, Unit = "Hz" },
    new() { Name = "PanelType", DataType = "string", IsRequired = false },

    // Keyboard
    new() { Name = "SwitchType", DataType = "string", IsRequired = false },

    // Mouse
    new() { Name = "DPI", DataType = "int", IsRequired = true },
    new() { Name = "ConnectionType", DataType = "string", IsRequired = true },

    // Laptop
    new() { Name = "CPUModel", DataType = "string", IsRequired = true },
    new() { Name = "GPUModel", DataType = "string", IsRequired = false },
    new() { Name = "ScreenSizeInch", DataType = "float", IsRequired = true, Unit = "inch" },
    new() { Name = "RAMCapacityGB", DataType = "int", IsRequired = true, Unit = "GB" },
    new() { Name = "StorageType", DataType = "string", IsRequired = true }
};
            return attributes;
        }
        public List<ProductTypeAttribute> ProductTypeAttributeGenerator()
        {
            var productTypeAttributes = new List<ProductTypeAttribute>
{
    // CPU (1)
    new(1, 1), new(1, 2), new(1, 3), new(1, 4), new(1, 5), new(1, 6), new(1, 7),

    // GPU (2)
    new(2, 1), new(2, 8), new(2, 9), new(2, 10), new(2, 11), new(2, 12),

    // Motherboard (3)
    new(3, 1), new(3, 6), new(3, 8), new(3, 13), new(3, 14), new(3, 15), new(3, 16), new(3, 17),

    // RAM (4)
    new(4, 1), new(4, 9), new(4, 14), new(4, 18), new(4, 19), new(4, 20), new(4, 21),

    // SSD (5)
    new(5, 1), new(5, 22), new(5, 23), new(5, 24), new(5, 25),

    // HDD (6)
    new(6, 1), new(6, 22), new(6, 23), new(6, 24), new(6, 25),

    // PowerSupply (7)
    new(7, 1), new(7, 12), new(7, 26),

    // Case (8)
    new(8, 1), new(8, 27), new(8, 28),

    // CoolingSystem (9)
    new(9, 1), new(9, 12), new(9, 29), new(9, 30),

    // Monitor (10)
    new(10, 1), new(10, 31), new(10, 32), new(10, 33), new(10, 34),

    // Keyboard (11)
    new(11, 1), new(11, 35), new(11, 21),

    // Mouse (12)
    new(12, 1), new(12, 36), new(12, 37), new(12, 21),

    // Laptop (13)
    new(13, 1), new(13, 38), new(13, 39), new(13, 40), new(13, 41), new(13, 42), new(13, 12)
};
            return productTypeAttributes;
        }

        public string GenerateAttributeValue(AttributeDefinition attributeDefinition)
        {
            return attributeDefinition.DataType switch
            {
                "string" => new Faker().Commerce.ProductName(),
                "int" => new Faker().Random.Int(1, 100).ToString(),
                "float" => new Faker().Random.Float(1, 10).ToString("F2"),
                _ => string.Empty
            };
        }
        public List<ProductAttribute> ProductAttributeGenerator(List<Product> products, List<ProductTypeAttribute> productTypeAttributes, List<AttributeDefinition> attributeDefinitions)
        {
            var result = new List<ProductAttribute>();
            foreach (var product in products)
            {
                var attrIds = productTypeAttributes
                    .Where(aId => aId.ProductTypeId == product.ProductTypeId)
                    .Select(aId => aId.AttributeDefinitionId)
                    .ToList();
                foreach (var attrId in attrIds)
                {
                    var attrDef = attributeDefinitions.FirstOrDefault(x => x.Id == attrId);
                    if (attrDef is not null)
                    {
                        var attribute = new ProductAttribute
                        {
                            ProductId = product.ProductId,
                            AttributeDefinitionId = attrDef.Id,
                            Value = GenerateAttributeValue(attrDef)
                        };
                        result.Add(attribute);
                    }
                }
            }
            return result;
        }

        public List<Coupon> CouponGenerator(int count)
        {
            var couponFaker = new Faker<Coupon>()
                .RuleFor(c => c.CouponIsPercentage, f => f.Random.Bool())
                .RuleFor(c => c.CouponValue, (f, c) =>
                    c.CouponIsPercentage
                        ? f.Random.Decimal(5, 30)
                        : f.Random.Decimal(250, 1000))
                .RuleFor(c => c.CouponMaxUsage, f => f.Random.Int(50, 500))
                .RuleFor(c => c.CouponMaxUsagePerUser, f => f.Random.Int(1, 5))
                .RuleFor(c => c.CouponMinOrderAmount, f => f.Random.Int(100, 1000))
                .RuleFor(c => c.CreateDate, f => f.Date.Past(1))
                .RuleFor(c => c.CouponStartTime, (f, c) => c.CreateDate.AddDays(f.Random.Int(0, 10)))
                .RuleFor(c => c.CouponIsActive, f => f.Random.Bool(0.8f))
                .RuleFor(c => c.CouponEndTime, (f, c) =>
                    c.CouponIsActive
                        ? DateTime.Now.AddDays(f.Random.Int(1, 60))
                        : DateTime.Now.AddDays(f.Random.Int(-60, -1)))
                .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
                .RuleFor(c => c.CouponCode, f => f.Random.AlphaNumeric(10).ToUpper())
                .RuleFor(c => c.CouponTargetType, f => f.PickRandom<CouponTargetType>());
            List<Coupon> result = new List<Coupon>();
            result = couponFaker.Generate(count);
            return result;
        }

        public List<Discount> DiscountGenerator(int count)
        {
            var discountFaker = new Faker<Discount>()
                .RuleFor(d => d.DiscountName, f => f.Commerce.Categories(1)[0])
                .RuleFor(d => d.CreateDate, f => f.Date.Past(1))
                .RuleFor(d => d.DiscountIsActive, f => f.Random.Bool(0.85f))
                .RuleFor(d => d.DiscountStartDate, (f, d) => d.CreateDate.AddDays(f.Random.Int(0, 10)))
                .RuleFor(d => d.DiscountEndDate, (f, d) =>
                    d.DiscountIsActive
                        ? DateTime.Now.AddDays(f.Random.Int(1, 60))
                        : DateTime.Now.AddDays(f.Random.Int(-60, -1)))
                .RuleFor(d => d.DiscountIsPercentage, f => f.Random.Bool())
                .RuleFor(d => d.DiscountRate, (f, d) =>
                    d.DiscountIsPercentage
                        ? f.Random.Decimal(5, 30)
                        : f.Random.Decimal(250, 1000))
                .RuleFor(d => d.Description, f => f.Commerce.ProductDescription());

            var result = new List<Discount>();
            result = discountFaker.Generate(count);
            return result;
        }

        public List<CouponProduct> CouponProductGenerator(List<int> CouponIds, List<int> ProductIds)
        {
            var result = new List<CouponProduct>();
            var availableProducts = new List<int>(ProductIds);

            foreach (var couponId in CouponIds)
            {
                int count = Random.Shared.Next(3, 8);

                for (int j = 0; j < count && availableProducts.Count > 0; j++)
                {
                    int randIndex = Random.Shared.Next(0, availableProducts.Count);
                    int productId = availableProducts[randIndex];
                    availableProducts.RemoveAt(randIndex);

                    result.Add(new CouponProduct
                    {
                        CouponId = couponId,
                        ProductId = productId
                    });
                }
            }

            return result;
        }


        public List<CouponBrand> CouponBrandGenerator(List<int> CouponIds, List<int> BrandIds)
        {
            var result = new List<CouponBrand>();
            var availableBrands = new List<int>(BrandIds);
            foreach (var id in CouponIds)
            {
                var i = Random.Shared.Next(1, 4);
                for (int j = 0; j < i; j++)
                {
                    var randIndex = Random.Shared.Next(0, availableBrands.Count);
                    var randBrandId = availableBrands[randIndex];
                    availableBrands.RemoveAt(randIndex);
                    var newRecord = new CouponBrand
                    {
                        CouponId = id,
                        BrandId = randBrandId
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<CouponCategory> CouponCategoriesGenerator(List<int> CouponIds, List<int> CategoryIds)
        {
            var result = new List<CouponCategory>();
            var availableCategories = new List<int>(CategoryIds);
            foreach (var id in CouponIds)
            {
                var i = Random.Shared.Next(1, 4);
                for (int j = 0; j < i; j++)
                {
                    var randIndex = Random.Shared.Next(0, availableCategories.Count);
                    var randCategoryId = availableCategories[randIndex];
                    availableCategories.RemoveAt(randIndex);
                    var newRecord = new CouponCategory
                    {
                        CouponId = id,
                        CategoryId = randCategoryId
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<CouponProductType> CouponProductTypeGenerator(List<int> CouponIds, List<int> TypeIds)
        {
            var result = new List<CouponProductType>();
            var availableTypeIds = new List<int>(TypeIds);
            foreach (var id in CouponIds)
            {
                var randIndex = Random.Shared.Next(0, availableTypeIds.Count);
                var randTypeId = availableTypeIds[randIndex];
                availableTypeIds.RemoveAt(randIndex);
                var newRecord = new CouponProductType
                {
                    CouponId = id,
                    ProductTypeId = randTypeId
                };
                result.Add(newRecord);
            }
            return result;
        }

        public List<DiscountProduct> DiscountProductGenerator(List<int> DiscountIds, List<int> ProductIds)
        {
            var result = new List<DiscountProduct>();
            var availableProducts = new List<int>(ProductIds);
            foreach (var id in DiscountIds)
            {
                var i = Random.Shared.Next(5, 11);
                for (int j = 0; j < i; j++)
                {
                    var randomIndex = Random.Shared.Next(0, availableProducts.Count);
                    var randomProductId = ProductIds[randomIndex];
                    availableProducts.RemoveAt(randomIndex);
                    var newRecord = new DiscountProduct
                    {
                        DiscountId = id,
                        ProductId = randomProductId
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<FollowedProduct> FollowedProductGenerator(List<string> UserIds, List<int> ProductIds)
        {
            var result = new List<FollowedProduct>();
            foreach (var id in UserIds)
            {
                var availableProducts = new List<int>(ProductIds);
                var i = Random.Shared.Next(1, 6);
                for (int k = 0; k < i; k++)
                {
                    var randomIndex = Random.Shared.Next(0, availableProducts.Count);
                    var randomProductId = availableProducts[randomIndex];
                    availableProducts.RemoveAt(randomIndex);
                    var newRecord = new FollowedProduct
                    {
                        UserId = id,
                        ProductId = randomProductId
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<ShoppingCartItem> ShoppingCartItemGenerator(List<string> UserIds, List<int> ProductIds)
        {
            var result = new List<ShoppingCartItem>();
            foreach (var id in UserIds)
            {
                var availableProducts = new List<int>(ProductIds);
                var i = Random.Shared.Next(1, 4);
                for (var k = 0; k < i; k++)
                {
                    var randomIndex = Random.Shared.Next(0, availableProducts.Count);
                    var randomProductId = availableProducts[randomIndex];
                    availableProducts.RemoveAt(randomIndex);
                    var newRecord = new ShoppingCartItem
                    {
                        UserId = id,
                        ProductId = randomProductId,
                        ItemCount = Random.Shared.Next(1, 3)
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<StatusName> StatusNameGenerator()
        {
            var result = new List<StatusName>();
            var statusList = new List<string>
            {
                "Pending",
                "Paid",
                "Processing",
                "Shipped",
                "Delivered",
                "Cancelled"
            };
            foreach (var status in statusList) 
            {
                var newRecord = new StatusName
                {
                    StatusNameString = status
                };
                result.Add(newRecord);
            }
            return result;
        }

        public List<OrderStatus> OrderStatusGenerator(List<Order> Orders) 
        {
            var result = new List<OrderStatus>();
            foreach(var order in Orders) 
            {
                int timeCounter = -1;
                var i = Random.Shared.Next(1,7);
                for(int j = 1; j <= i; j++) 
                {
                    if (i == 6 && j == 5)
                        continue;
                    var newRecord = new OrderStatus
                    {
                        OrderId = order.OrderId,
                        StatusDate = order.OrderDate.AddHours(timeCounter + j),
                        StatusNameId = j
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }
    }
}
