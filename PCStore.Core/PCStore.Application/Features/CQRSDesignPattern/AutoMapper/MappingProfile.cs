using AutoMapper;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CreateShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountUsageCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponBrandResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponCategoryResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductTypeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Results;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using PCStore.Domain.Entities;

namespace PCStore.Application.Features.CQRSDesignPattern.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetProductInformationsResult>();
            CreateMap<Product, GetAllProductsResult>()
                .ForMember(x => x.ProductTypeName, o => o.MapFrom(s => s.ProductType!.Name))
                .ForMember(x => x.ProductBrandName, o => o.MapFrom(s => s.Brand!.BrandName))
                .ForMember(x => x.ProductCategoryName, o => o.MapFrom(s => s.Category!.CategoryName))
                .ForMember(x => x.ProductMainPhotoPath, o => o.MapFrom(s => s.ProductMainPhotoPath))
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.ProductName))
                .ForMember(x => x.ProductIsAvailable, o => o.MapFrom(s => s.ProductIsAvailable))
                .ForMember(x => x.ProductRateScore, o => o.MapFrom(s => s.ProductRateScore))
                .ForMember(x => x.ProductStock, o => o.MapFrom(s => s.ProductStock))
                .ForMember(x => x.ProductTotalRate, o => o.MapFrom(s => s.ProductTotalRate));
            CreateMap<Comment, GetCommentsByProductIdResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName));
            CreateMap<Answer, GetAnswersByCommentIdResult>()
                .ForMember(dest => dest.AnswerUserName, opt => opt.MapFrom(src => src.User!.UserName));
            CreateMap<CreateAnswerCommand, Answer>()
                .ForMember(d => d.User, o => o.Ignore());
            CreateMap<Answer, CreateAnswerResult>()
                .ForMember(d => d.AnswerUserName, o => o.MapFrom(s => s.User!.UserName));
            CreateMap<Answer, UpdateAnswerResult>()
                .ForMember(d => d.AnswerUserName, o => o.MapFrom(s => s.User!.UserName));
            CreateMap<CreateAnswerVoteCommand, AnswerVote>();
            CreateMap<CreateCommentVoteCommand, CommentVote>();
            CreateMap<ProductAttribute, GetProductAttributesResult>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.AttributeDefinition!.Name))
                .ForMember(x => x.Unit, o => o.MapFrom(s => s.AttributeDefinition!.Unit));
            CreateMap<ProductTypeAttribute, GetTypeAttributesByIdResult>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.AttributeDefinition!.Name))
                .ForMember(x => x.Unit, o => o.MapFrom(s => s.AttributeDefinition!.Unit))
                .ForMember(x => x.IsRequired, o => o.MapFrom(s => s.AttributeDefinition!.IsRequired))
                .ForMember(x => x.DataType, o => o.MapFrom(s => s.AttributeDefinition!.DataType));
            CreateMap<CreateAttributeDefinitionCommand, AttributeDefinition>();
            CreateMap<AttributeDefinition, CreateAttributeDefinitionResult>();
            CreateMap<ProductType, CreateProductTypeResult>();
            CreateMap<Comment, UpdateCommentResult>();
            CreateMap<AttributeDefinition, UpdateAttributeDefinitionResult>();
            CreateMap<ProductType, UpdateProductTypeResult>();
            CreateMap<Product, CreateProductResult>()
                .ForMember(x => x.BrandName, o => o.MapFrom(s => s.Brand!.BrandName))
                .ForMember(x => x.CategoryName, o => o.MapFrom(s => s.Category!.CategoryName));
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductPhoto, GetPhotosByProductIdResult>();
            CreateMap<Product, CreateProductResult>();
            CreateMap<UpdateProductCommand, Product>() // UPDATE İÇİN MAPLEME
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, UpdateProductResult>()
                .ForMember(x => x.BrandName, o => o.MapFrom(x => x.Brand!.BrandName))
                .ForMember(x => x.CategoryName, o => o.MapFrom(x => x.Category!.CategoryName));
            CreateMap<ProductPhoto, UpdatePhotosResult>();
            CreateMap<ProductAttribute, UpdateProductAttributeResult>()
                .ForMember(x => x.Unit, o => o.MapFrom(s => s.AttributeDefinition!.Unit))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.AttributeDefinition!.Name));
            CreateMap<ProductAttribute, CreateProductAttributesResult>()
                .ForMember(x => x.Unit, o => o.MapFrom(s => s.AttributeDefinition!.Unit))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.AttributeDefinition!.Name));
            CreateMap<User, UpdateUserResult>();
            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<Comment, CreateCommentResult>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.User!.Name))
                .ForMember(x => x.Surname, o => o.MapFrom(s => s.User!.Surname));
            CreateMap<FollowedProduct, CreateFollowedProductResult>()
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product!.ProductName))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.Product!.ProductPrice));
            //CreateMap<FollowedProduct, GetFollowedProductsResult>()
            //    .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product!.ProductName))
            //    .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.Product!.ProductPrice))
            //    .ForMember(x => x.ProductPhotoPath, o => o.MapFrom(s => s.Product!.ProductMainPhotoPath))
            //    .ForMember(x => x.ProductId, o => o.MapFrom(s => s.Product!.ProductId))
            //    .ForMember(x => x.ProductRateScore, o => o.MapFrom(s => s.Product!.ProductRateScore))
            //    .ForMember(x => x.ProductTotalRate, o => o.MapFrom(s => s.Product!.ProductTotalRate));
            CreateMap<Product, GetFollowedProductsResult>();
            CreateMap<CreateShopCartItemCommand, ShoppingCartItem>();
            CreateMap<ShoppingCartItem, GetShopCartItemsResult>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.Product!.ProductId))
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product!.ProductName))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.Product!.ProductPrice))
                .ForMember(x => x.ProductMainPhotoPath, o => o.MapFrom(s => s.Product!.ProductMainPhotoPath))
                .ForMember(x => x.ProductRateScore, o => o.MapFrom(s => s.Product!.ProductRateScore))
                .ForMember(x => x.ProductTotalRate, o => o.MapFrom(s => s.Product!.ProductTotalRate));
            CreateMap<CreateCouponCommand, Coupon>();
            CreateMap<Coupon, CreateCouponResult>();
            CreateMap<GetAllProductsResult, GetProductInformationsResult>();
            CreateMap<Coupon, AdminGetCouponByIdResult>();
            CreateMap<Coupon, AdminGetAllCouponsResult>();
            CreateMap<Coupon, UpdateCouponResult>();
            CreateMap<CouponProduct, ListCreateCouponProductResult>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.Product!.ProductId))
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product!.ProductName))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.Product!.ProductPrice))
                .ForMember(x => x.ProductMainPhotoPath, o => o.MapFrom(s => s.Product!.ProductMainPhotoPath))
                .ForMember(x => x.ProductStock, o => o.MapFrom(s => s.Product!.ProductStock));
            CreateMap<CreateCouponBrandCommand, CouponBrand>();
            CreateMap<Brand, CreateCouponBrandResult>();
            CreateMap<CreateCouponCategoryCommand, CouponCategory>();
            CreateMap<CouponCategory, CreateCouponCategoryResult>()
                .ForMember(x => x.CategoryName, o => o.MapFrom(s => s.Category!.CategoryName));
            CreateMap<CreateCouponProductTypeCommand, CouponProductType>();
            CreateMap<CouponProductType, CreateCouponProductTypeResult>();
            CreateMap<GetAllProductsResult, GetFollowedProductsResult>();
            CreateMap<CreateDiscountCommand, Discount>();
            CreateMap<Discount, GetAllDiscountsResult>();
            CreateMap<Discount, UpdateDiscountResult>();
            CreateMap<Discount, CreateDiscountResult>();
            CreateMap<GetAllProductsResult, CreateDiscountProductResult>();
            CreateMap<GetAllProductsResult, GetAllDiscountProductsResult>();
            CreateMap<GetAllProductsResult, GetShopCartItemsResult>();
            CreateMap<GetAllProductsResult, CouponValidatorCommand>();
            CreateMap<CouponValidatorResult, GetShopCartItemsResult>();
            CreateMap<DiscountValidatorCommand, DiscountValidatorResult>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice))
                .ForMember(x => x.DiscountRate, o => o.Ignore())
                .ForMember(x => x.IsDiscountPercentage, o => o.Ignore())
                .ForMember(x => x.OldPrice, o => o.Ignore());
            CreateMap<Product, DiscountValidatorCommand>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice));
            CreateMap<DiscountValidatorResult, GetAllProductsResult>()
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice))
                .ForMember(x => x.OldPrice, o => o.MapFrom(s => s.OldPrice))
                .ForMember(x => x.IsDiscountPercentage, o => o.MapFrom(s => s.IsDiscountPercentage))
                .ForMember(x => x.DiscountRate, o => o.MapFrom(s => s.DiscountRate))
                .ForMember(x => x.ProductMainPhotoPath, o => o.Ignore())
                .ForMember(x => x.ProductId, o => o.Ignore())
                .ForMember(x => x.ProductName, o => o.Ignore())
                .ForMember(x => x.ProductBrandName, o => o.Ignore())
                .ForMember(x => x.ProductCategoryName, o => o.Ignore())
                .ForMember(x => x.ProductIsAvailable, o => o.Ignore())
                .ForMember(x => x.ProductRateScore, o => o.Ignore())
                .ForMember(x => x.ProductStock, o => o.Ignore())
                .ForMember(x => x.ProductTotalRate, o => o.Ignore())
                .ForMember(x => x.ProductTypeName, o => o.Ignore());
            CreateMap<DiscountValidatorResult, CreateDiscountProductResult>();
            CreateMap<DiscountValidatorResult, GetAllDiscountProductsResult>();
            CreateMap<DiscountValidatorResult, GetFollowedProductsResult>();
            CreateMap<DiscountValidatorResult, GetProductInformationsResult>();
            CreateMap<DiscountValidatorResult, GetShopCartItemsResult>();
            CreateMap<DiscountValidatorResult, CouponValidatorCommand>();
            CreateMap<Product, GetShopCartItemsResult>();
            CreateMap<ShoppingCartItem, GetShopCartItemsResult>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.ProductMainPhotoPath, o => o.MapFrom(s => s.Product!.ProductMainPhotoPath))
                .ForMember(x => x.ProductRateScore, o => o.MapFrom(s => s.Product!.ProductRateScore))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.Product!.ProductPrice))
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.Product!.ProductId))
                .ForMember(x => x.ItemCount, o => o.MapFrom(s => s.ItemCount))
                .ForMember(x => x.ProductName, o => o.MapFrom(s => s.Product!.ProductName))
                .ForMember(x => x.ProductTotalRate, o => o.MapFrom(s => s.Product!.ProductTotalRate))
                .ForMember(x => x.CategoryName, o => o.MapFrom(s => s.Product!.Category!.CategoryName))
                .ForMember(x => x.BrandName, o => o.MapFrom(s => s.Product!.Brand!.BrandName));
            CreateMap<GetShopCartItemsResult, CouponValidatorCommand>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice))
                .ForMember(x => x.ProductBrandId, o => o.Ignore())
                .ForMember(x => x.ProductCategoryId, o => o.Ignore())
                .ForMember(x => x.ProductTypeId, o => o.Ignore());
            CreateMap<CreateCouponUsageCommand, CouponUsage>()
                .ForMember(x => x.CouponUsageCouponId, o => o.MapFrom(s => s.CouponId))
                .ForMember(x => x.CouponUsageUserId, o => o.MapFrom(s => s.UserId))
                .ForMember(x => x.CouponUsageOrderId, o => o.MapFrom(s => s.OrderId))
                .ForMember(x => x.DiscountTotal, o => o.MapFrom(s => s.DiscountTotal));
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<Order, CreateOrderResult>();
            CreateMap<GetShopCartItemsResult, OrderProductListDTO>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(x => x.ProductOldPrice, o => o.MapFrom(s => s.OldPrice))
                .ForMember(x => x.ProductOldTotalCost, o => o.MapFrom(s => s.OldTotalPrice))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice))
                .ForMember(x => x.ProductQuantity, o => o.MapFrom(s => s.ItemCount))
                .ForMember(x => x.ProductTotalCost, o => o.MapFrom(s => s.TotalPrice));
            CreateMap<OrderProductListDTO, OrderProductList>()
                .ForMember(x => x.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(x => x.ProductOldPrice, o => o.MapFrom(s => s.ProductOldPrice))
                .ForMember(x => x.ProductOldTotalCost, o => o.MapFrom(s => s.ProductOldTotalCost))
                .ForMember(x => x.ProductPrice, o => o.MapFrom(s => s.ProductPrice))
                .ForMember(x => x.ProductQuantity, o => o.MapFrom(s => s.ProductQuantity))
                .ForMember(x => x.ProductTotalCost, o => o.MapFrom(s => s.ProductTotalCost));
            CreateMap<DiscountUsageCalculatorResult, DiscountUsage>()
                .ForMember(x => x.DiscountId, o => o.MapFrom(s => s.DiscountId))
                .ForMember(x => x.DiscountTotal, o => o.MapFrom(s => s.DiscountTotal));
        }
    }
}
