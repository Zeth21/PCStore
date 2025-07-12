using Bogus;
using PCStore.Domain.Entities;
using PCStore.Domain.Enum;

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
                .RuleFor(a => a.District, f => f.Address.City())
                .RuleFor(a => a.Neighborhood, f => f.Address.City())
                .RuleFor(a => a.BuildingNumber, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Floor, f => f.PickRandom("1", "2", "3", "4"))
                .RuleFor(a => a.Description, f => f.Address.FullAddress())
                .RuleFor(a => a.Street, f => f.Address.City());
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
                .RuleFor(a => a.OrderTotalCost, f => f.Random.Int(1000, 10000))
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


    }
}
