using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PCStore.Application.Abstractions.Auth;
using PCStore.Application.Features.CQRSDesignPattern.AutoMapper;
using PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers;
using PCStore.Application.Features.Helpers;
using PCStore.Application.Features.Helpers.Factories;
using PCStore.Application.Features.Helpers.Helper;
using PCStore.Application.Features.Helpers.Validators.CouponValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.RoleControl;
using PCStore.Application.Services.AnswerService;
using PCStore.Application.Services.AnswerVoteService;
using PCStore.Application.Services.AttributeDefinitionService;
using PCStore.Application.Services.BrandService;
using PCStore.Application.Services.CommentService;
using PCStore.Application.Services.CommentVoteService;
using PCStore.Application.Services.CouponProductService;
using PCStore.Application.Services.CouponService;
using PCStore.Application.Services.DiscountProductService;
using PCStore.Application.Services.DiscountService;
using PCStore.Application.Services.EmailService;
using PCStore.Application.Services.EmailService.ServiceDTO;
using PCStore.Application.Services.FollowedProductsService;
using PCStore.Application.Services.NotificationService;
using PCStore.Application.Services.OrderService;
using PCStore.Application.Services.ProductAttributeService;
using PCStore.Application.Services.ProductPhotoService;
using PCStore.Application.Services.ProductService;
using PCStore.Application.Services.ShoppingCartItemService;
using PCStore.Application.Services.TypeService;
using PCStore.Application.Services.UserService;
using PCStore.Domain.Entities;
using PCStore.Domain.IdentityFaker;
using PCStore.Persistence;
using PCStore.Persistence.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ProjectDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDbContext<ProjectDbContext>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PCStore.API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. Örn: 'Bearer {token}'"
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:44322")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductsHandler).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ISeedData, SeedData>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();
builder.Services.AddScoped<IFakerGenerator, FakerGenerator>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IAnswerVoteService, AnswerVoteService>();
builder.Services.AddScoped<ICommentVoteService, CommentVoteService>();
builder.Services.AddScoped<IAttributeDefinitionService, AttributeDefinitionService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IProductAttributeService, ProductAttributeService>();
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFollowedService, FollowedService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IDiscountChecker, DiscountChecker>();
builder.Services.AddScoped<ICouponValidator, AllProductsCouponValidator>();
builder.Services.AddScoped<ICouponValidator, BrandCouponValidator>();
builder.Services.AddScoped<ICouponValidator, CategoryCouponValidator>();
builder.Services.AddScoped<ICouponValidator, SpecificProductsCouponValidator>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IDiscountProductService, DiscountProductService>();
builder.Services.AddScoped<ICouponValidatorFactory, CouponValidatorFactory>();
builder.Services.AddScoped<IDiscountUsageCalculator, DiscountUsageCalculator>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddAuthorization();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    var seeder2 = scope.ServiceProvider.GetRequiredService<ISeedData>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await seeder.Seed();
    await seeder2.SeedRolesAsync(roleManager);
    await seeder2.SeedUsersAsync(userManager);
}


app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();

