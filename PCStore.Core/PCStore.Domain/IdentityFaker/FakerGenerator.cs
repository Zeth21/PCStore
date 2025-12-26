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
            var usedNames = new HashSet<string>();
            var usedDescriptions = new HashSet<string>();
            var addressFaker = new Faker<Address>()
                .RuleFor(a => a.UserId, f => f.PickRandom(users).Id)
                .RuleFor(a => a.AddressName, f =>
                {
                    string name;
                    do
                    {
                        name = f.Address.StreetName();
                        if (name.Length > 50) name = name.Substring(0, 50);
                    } while (!usedNames.Add(name));
                    return name;
                })
                .RuleFor(a => a.Description, f =>
                {
                    string desc;
                    do
                    {
                        desc = f.Address.FullAddress() ?? "No Description";
                    } while (!usedDescriptions.Add(desc));
                    return desc;
                });

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
            var usedPairs = new HashSet<(string, int)>();
            var random = new Random();

            var answerVoteFaker = new Faker<AnswerVote>()
                .RuleFor(a => a.AnswerVoteValue, f => f.PickRandom<VoteType>());

            for (int i = 0; i < count; i++)
            {
                string userId;
                int answerId;
                do
                {
                    userId = users[random.Next(users.Count)].Id;
                    answerId = answers[random.Next(answers.Count)].AnswerId;
                } while (!usedPairs.Add((userId, answerId)));

                var answerVote = answerVoteFaker.Generate();
                answerVote.AnswerVoteUserId = userId;
                answerVote.AnswerVoteAnswerId = answerId;

                result.Add(answerVote);
            }
            return result;
        }

        List<Brand> IFakerGenerator.BrandGenerator(int count)
        {
            var result = new List<Brand>();
            var usedNames = new HashSet<string>();
            var brandFaker = new Faker<Brand>()
                .RuleFor(a => a.BrandName, f =>
                {
                    string name;
                    do
                    {
                        name = f.Company.CompanyName();
                    } while (!usedNames.Add(name));
                    return name;
                });

            for (int i = 0; i < count; i++)
            {
                var brand = brandFaker.Generate();
                result.Add(brand);
            }
            return result;
        }

        List<Category> IFakerGenerator.CategoryGenerator(int count)
        {
            var result = new List<Category>();
            var usedNames = new HashSet<string>();
            var faker = new Faker();
            var random = new Random();
            int idCounter = 0;
            int maxAttempts = 100;

            for (int i = 0; i < count; i++)
            {
                string name = null;
                int attempts = 0;
                do
                {
                    name = faker.Commerce.Categories(1).First();
                    attempts++;
                    if (attempts > maxAttempts)
                    {
                        name = $"Category_{i}";
                        break;
                    }
                } while (usedNames.Contains(name));
                usedNames.Add(name);

                var category = new Category
                {
                    CategoryName = name
                };
                idCounter++;
                if (idCounter > 10 && random.NextDouble() > 0.5)
                    category.ParentCategoryId = random.Next(1, idCounter);
                result.Add(category);
            }
            return result;
        }

        List<Comment> IFakerGenerator.CommentGenerator(int count, List<Product> products, List<User> users)
        {
            var result = new List<Comment>();
            var commentFaker = new Faker<Comment>()
                .RuleFor(a => a.CommentText, f =>
                {
                    var text = f.Lorem.Sentence(10);
                    return text.Length > 200 ? text.Substring(0, 200) : text;
                })
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
            var usedPairs = new HashSet<(string, int)>();
            var random = new Random();
            var commentVoteFaker = new Faker<CommentVote>()
                .RuleFor(a => a.CommentVoteValue, f => f.PickRandom<VoteType>());

            for (int i = 0; i < count; i++)
            {
                string userId;
                int commentId;
                do
                {
                    userId = users[random.Next(users.Count)].Id;
                    commentId = comments[random.Next(comments.Count)].CommentId;
                } while (!usedPairs.Add((userId, commentId)));

                var commentVote = commentVoteFaker.Generate();
                commentVote.CommentVoteUserId = userId;
                commentVote.CommentVoteCommentId = commentId;
                result.Add(commentVote);
            }
            return result;
        }

        List<Notification> IFakerGenerator.NotificationGenerator(int count, List<User> users)
        {
            var result = new List<Notification>();
            var notificationFaker = new Faker<Notification>()
                .RuleFor(a => a.NotificationType, f => f.PickRandom<NotifType>())
                .RuleFor(a => a.NotificationTitle, f =>
                {
                    var title = f.Hacker.Phrase();
                    return title.Length > 100 ? title.Substring(0, 100) : title;
                })
                .RuleFor(a => a.NotificationStatus, f => f.Random.Bool())
                .RuleFor(a => a.NotificationContent, f =>
                {
                    var content = f.Lorem.Paragraph(2);
                    return content.Length > 500 ? content.Substring(0, 500) : content;
                })
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
                .RuleFor(a => a.OrderIsActive, f => f.Random.Bool());
            for (int i = 0; i < count; i++)
            {
                var user = new Faker().PickRandom(users);
                var order = orderFaker.Generate();
                order.OrderAddressId = new Faker().PickRandom(user.Addresses).Id;
                order.OrderUserId = user.Id;
                result.Add(order);
            }
            return result;
        }

        List<ProductPhoto> IFakerGenerator.ProductPhotoGenerator(List<Product> products)
        {
            var result = new List<ProductPhoto>();
            var usedNames = new HashSet<string>();
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string photoName;
                    do
                    {
                        photoName = $"{products[i].ProductId}_Photo_{j}";
                    } while (!usedNames.Add(photoName));
                    var photo = new ProductPhoto
                    {
                        PhotoPath = photoName,
                        PhotoName = photoName,
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
            var usedPairs = new HashSet<(string, int)>();
            var rnd = new Random();
            var rateFaker = new Faker<ProductRate>()
                .RuleFor(a => a.ProductRateScore, f => f.Random.Decimal(0, 10))
                .RuleFor(a => a.ProductRateUserId, f => f.PickRandom(users).Id);
            for (int i = 0; i < products.Count; i++)
            {
                int rateCount = rnd.Next(5, 20);
                for (int j = 0; j < rateCount; j++)
                {
                    string userId = users[rnd.Next(users.Count)].Id;
                    int productId = products[i].ProductId;
                    if (!usedPairs.Add((userId, productId))) continue;
                    var rate = rateFaker.Generate();
                    rate.ProductRateProductId = productId;
                    rate.ProductRateUserId = userId;
                    result.Add(rate);
                }
            }
            return result;
        }

        List<User> IFakerGenerator.UserGenerator(int count)
        {
            var result = new List<User>();
            var usedUserNames = new HashSet<string>();
            var usedEmails = new HashSet<string>();
            var userFaker = new Faker<User>()
                .RuleFor(a => a.Name, f => f.Person.FirstName)
                .RuleFor(a => a.Surname, f => f.Person.LastName)
                .RuleFor(a => a.UserName, f =>
                {
                    string userName;
                    do
                    {
                        userName = f.Internet.UserName();
                    } while (!usedUserNames.Add(userName));
                    return userName;
                })
                .RuleFor(a => a.Email, f =>
                {
                    string email;
                    do
                    {
                        email = f.Internet.Email();
                    } while (!usedEmails.Add(email));
                    return email;
                })
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber());

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
            var usedNames = new HashSet<string>();
            var productFaker = new Faker<Product>()
                .RuleFor(a => a.ProductName, f =>
                {
                    string name;
                    do
                    {
                        name = f.Commerce.ProductName();
                    } while (!usedNames.Add(name));
                    return name;
                })
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
    new() { Name = "LaptopSize", DataType = "float", IsRequired = true, Unit = "inch" },
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


        public List<ProductAttribute> ProductAttributeGenerator(List<Product> products, List<ProductTypeAttribute> productTypeAttributes, List<AttributeDefinition> attributeDefinitions)
        {
            var result = new List<ProductAttribute>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var product in products)
            {
                var attrIds = productTypeAttributes
                    .Where(aId => aId.ProductTypeId == product.ProductTypeId)
                    .Select(aId => aId.AttributeDefinitionId)
                    .ToList();
                foreach (var attrId in attrIds)
                {
                    if (!usedPairs.Add((attrId, product.ProductId))) continue;
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

        public List<Coupon> CouponGenerator(int count)
        {
            var result = new List<Coupon>();
            var usedCodes = new HashSet<string>();
            var couponFaker = new Faker<Coupon>()
                .RuleFor(c => c.CouponIsPercentage, f => f.Random.Bool())
                .RuleFor(c => c.CouponValue, (f, c) =>
                    c.CouponIsPercentage
                        ? Math.Round(f.Random.Decimal(5, 30), 2)
                        : Math.Round(f.Random.Decimal(250, 1000), 2))
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
                .RuleFor(c => c.Description, f =>
                {
                    var desc = f.Commerce.ProductDescription();
                    return desc.Length > 200 ? desc.Substring(0, 200) : desc;
                })
                .RuleFor(c => c.CouponCode, f =>
                {
                    string code;
                    do
                    {
                        code = f.Random.AlphaNumeric(10).ToUpper();
                    } while (!usedCodes.Add(code));
                    return code;
                })
                .RuleFor(c => c.CouponTargetType, f => f.PickRandom<CouponTargetType>());
            result = couponFaker.Generate(count);
            return result;
        }

        public List<Discount> DiscountGenerator(int count)
        {
            var result = new List<Discount>();
            var usedNames = new HashSet<string>();
            var discountFaker = new Faker<Discount>()
                .RuleFor(d => d.DiscountName, f =>
                {
                    string name;
                    do
                    {
                        name = f.Commerce.Categories(1)[0];
                    } while (!usedNames.Add(name));
                    return name;
                })
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
                        ? Math.Round(f.Random.Decimal(5, 30), 2)
                        : Math.Round(f.Random.Decimal(250, 1000), 2))
                .RuleFor(d => d.Description, f => f.Commerce.ProductDescription());
            result = discountFaker.Generate(count);
            return result;
        }

        public List<CouponProduct> CouponProductGenerator(List<int> CouponIds, List<int> ProductIds)
        {
            var result = new List<CouponProduct>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var couponId in CouponIds)
            {
                int count = Random.Shared.Next(3, 8);
                for (int j = 0; j < count; j++)
                {
                    if (ProductIds.Count == 0) break;
                    int productId = ProductIds[Random.Shared.Next(ProductIds.Count)];
                    if (!usedPairs.Add((couponId, productId))) continue;
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
            var usedPairs = new HashSet<(int, int)>();
            foreach (var couponId in CouponIds)
            {
                int count = Random.Shared.Next(1, 4);
                for (int j = 0; j < count; j++)
                {
                    if (BrandIds.Count == 0) break;
                    int brandId = BrandIds[Random.Shared.Next(BrandIds.Count)];
                    if (!usedPairs.Add((couponId, brandId))) continue;
                    result.Add(new CouponBrand
                    {
                        CouponId = couponId,
                        BrandId = brandId
                    });
                }
            }
            return result;
        }

        public List<CouponCategory> CouponCategoriesGenerator(List<int> CouponIds, List<int> CategoryIds)
        {
            var result = new List<CouponCategory>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var couponId in CouponIds)
            {
                int count = Random.Shared.Next(1, 4);
                for (int j = 0; j < count; j++)
                {
                    if (CategoryIds.Count == 0) break;
                    int categoryId = CategoryIds[Random.Shared.Next(CategoryIds.Count)];
                    if (!usedPairs.Add((couponId, categoryId))) continue;
                    result.Add(new CouponCategory
                    {
                        CouponId = couponId,
                        CategoryId = categoryId
                    });
                }
            }
            return result;
        }

        public List<CouponProductType> CouponProductTypeGenerator(List<int> CouponIds, List<int> TypeIds)
        {
            var result = new List<CouponProductType>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var couponId in CouponIds)
            {
                int count = Random.Shared.Next(1, 4);
                for (int j = 0; j < count; j++)
                {
                    if (TypeIds.Count == 0) break;
                    int typeId = TypeIds[Random.Shared.Next(TypeIds.Count)];
                    if (!usedPairs.Add((couponId, typeId))) continue;
                    result.Add(new CouponProductType
                    {
                        CouponId = couponId,
                        ProductTypeId = typeId
                    });
                }
            }
            return result;
        }

        public List<DiscountProduct> DiscountProductGenerator(List<int> DiscountIds, List<int> ProductIds)
        {
            var result = new List<DiscountProduct>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var discountId in DiscountIds)
            {
                int count = Random.Shared.Next(5, 11);
                for (int j = 0; j < count; j++)
                {
                    if (ProductIds.Count == 0) break;
                    int productId = ProductIds[Random.Shared.Next(ProductIds.Count)];
                    if (!usedPairs.Add((discountId, productId))) continue;
                    result.Add(new DiscountProduct
                    {
                        DiscountId = discountId,
                        ProductId = productId
                    });
                }
            }
            return result;
        }

        public List<FollowedProduct> FollowedProductGenerator(List<string> UserIds, List<int> ProductIds)
        {
            var result = new List<FollowedProduct>();
            var usedPairs = new HashSet<(string, int)>();
            foreach (var userId in UserIds)
            {
                int count = Random.Shared.Next(1, 6);
                for (int k = 0; k < count; k++)
                {
                    if (ProductIds.Count == 0) break;
                    int productId = ProductIds[Random.Shared.Next(ProductIds.Count)];
                    if (!usedPairs.Add((userId, productId))) continue;
                    result.Add(new FollowedProduct
                    {
                        UserId = userId,
                        ProductId = productId
                    });
                }
            }
            return result;
        }

        public List<ShoppingCartItem> ShoppingCartItemGenerator(List<string> UserIds, List<int> ProductIds)
        {
            var result = new List<ShoppingCartItem>();
            var usedPairs = new HashSet<(string, int)>();
            foreach (var userId in UserIds)
            {
                int count = Random.Shared.Next(1, 4);
                for (int k = 0; k < count; k++)
                {
                    if (ProductIds.Count == 0) break;
                    int productId = ProductIds[Random.Shared.Next(ProductIds.Count)];
                    if (!usedPairs.Add((userId, productId))) continue;
                    result.Add(new ShoppingCartItem
                    {
                        UserId = userId,
                        ProductId = productId,
                        ItemCount = Random.Shared.Next(1, 3)
                    });
                }
            }
            return result;
        }

        public List<StatusName> StatusNameGenerator()
        {
            var result = new List<StatusName>();
            var usedNames = new HashSet<string>();
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
                if (usedNames.Add(status))
                {
                    var newRecord = new StatusName
                    {
                        StatusNameString = status
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        public List<OrderStatus> OrderStatusGenerator(List<Order> Orders)
        {
            var result = new List<OrderStatus>();
            var usedPairs = new HashSet<(int, int)>();
            foreach (var order in Orders)
            {
                int timeCounter = -1;
                var i = Random.Shared.Next(1, 7);
                for (int j = 1; j <= i; j++)
                {
                    if (i == 6 && j == 5)
                        continue;
                    if (!usedPairs.Add((order.OrderId, j))) continue;
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

        public List<OrderProductList> OrderProductListGenerator(List<Order> orders, List<Product> products)
        {
            var result = new List<OrderProductList>();
            var usedPairs = new HashSet<(int, int)>();
            var random = new Random();
            foreach(var order in orders) 
            {
                for (int i = 0; i < random.Next(1, 6); i++)
                {
                    int productId;
                    do
                    {
                        productId = products[random.Next(products.Count)].ProductId;
                    } while (!usedPairs.Add((order.OrderId, productId)));
                    var quantity = random.Next(1, 5);
                    var orderProduct = new OrderProductList
                    {
                        OrderId = order.OrderId,
                        ProductId = productId,
                        ProductQuantity = (byte)quantity,
                        ProductPrice = products.First(p => p.ProductId == productId).ProductPrice,
                        ProductTotalCost = products.First(p => p.ProductId == productId).ProductPrice * quantity
                    };
                    result.Add(orderProduct);
                }
            }
            return result;
        }
    }
}
