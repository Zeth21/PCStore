using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FluentAPIConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_AttributeDefinitionId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductRates_ProductRateUserId",
                table: "ProductRates");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeDefinitionId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_FollowedProducts_UserId",
                table: "FollowedProducts");

            migrationBuilder.DropIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages");

            migrationBuilder.DropIndex(
                name: "IX_DiscountProducts_DiscountId",
                table: "DiscountProducts");

            migrationBuilder.DropIndex(
                name: "IX_CouponProductTypes_CouponId",
                table: "CouponProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_CouponProducts_CouponId",
                table: "CouponProducts");

            migrationBuilder.DropIndex(
                name: "IX_CouponCategories_CouponId",
                table: "CouponCategories");

            migrationBuilder.DropIndex(
                name: "IX_CouponBrands_CouponId",
                table: "CouponBrands");

            migrationBuilder.DropIndex(
                name: "IX_CommentVotes_CommentVoteUserId",
                table: "CommentVotes");

            migrationBuilder.DropIndex(
                name: "IX_AnswerVotes_AnswerVoteUserId",
                table: "AnswerVotes");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "StatusNameString",
                table: "StatusNames",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coupons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CommentText",
                table: "Comments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AttributeDefinitions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StatusNames_StatusNameString",
                table: "StatusNames",
                column: "StatusNameString",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId_UserId",
                table: "ShoppingCartItems",
                columns: new[] { "ProductId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_AttributeDefinitionId_ProductTypeId",
                table: "ProductTypeAttributes",
                columns: new[] { "AttributeDefinitionId", "ProductTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_ProductRateUserId_ProductRateProductId",
                table: "ProductRates",
                columns: new[] { "ProductRateUserId", "ProductRateProductId" },
                unique: true,
                filter: "[ProductRateUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeDefinitionId_ProductId",
                table: "ProductAttributes",
                columns: new[] { "AttributeDefinitionId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_OrderId_StatusId",
                table: "OrderStatuses",
                columns: new[] { "OrderId", "StatusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowedProducts_UserId_ProductId",
                table: "FollowedProducts",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsages_OrderId_DiscountId",
                table: "DiscountUsages",
                columns: new[] { "OrderId", "DiscountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountName",
                table: "Discounts",
                column: "DiscountName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProducts_DiscountId_ProductId",
                table: "DiscountProducts",
                columns: new[] { "DiscountId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_CouponUsageOrderId_CouponUsageCouponId",
                table: "CouponUsages",
                columns: new[] { "CouponUsageOrderId", "CouponUsageCouponId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponProductTypes_CouponId_ProductTypeId",
                table: "CouponProductTypes",
                columns: new[] { "CouponId", "ProductTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponProducts_CouponId_ProductId",
                table: "CouponProducts",
                columns: new[] { "CouponId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponCategories_CouponId_CategoryId",
                table: "CouponCategories",
                columns: new[] { "CouponId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponBrands_CouponId_BrandId",
                table: "CouponBrands",
                columns: new[] { "CouponId", "BrandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentVoteUserId_CommentVoteCommentId",
                table: "CommentVotes",
                columns: new[] { "CommentVoteUserId", "CommentVoteCommentId" },
                unique: true,
                filter: "[CommentVoteUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true,
                filter: "[CategoryName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandName",
                table: "Brands",
                column: "BrandName",
                unique: true,
                filter: "[BrandName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDefinitions_Name",
                table: "AttributeDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVotes_AnswerVoteUserId_AnswerVoteAnswerId",
                table: "AnswerVotes",
                columns: new[] { "AnswerVoteUserId", "AnswerVoteAnswerId" },
                unique: true,
                filter: "[AnswerVoteUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Description",
                table: "Addresses",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId_AddressName",
                table: "Addresses",
                columns: new[] { "UserId", "AddressName" },
                unique: true,
                filter: "[AddressName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusNames_StatusNameString",
                table: "StatusNames");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ProductId_UserId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_AttributeDefinitionId_ProductTypeId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductName",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductRates_ProductRateUserId_ProductRateProductId",
                table: "ProductRates");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeDefinitionId_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_OrderId_StatusId",
                table: "OrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_FollowedProducts_UserId_ProductId",
                table: "FollowedProducts");

            migrationBuilder.DropIndex(
                name: "IX_DiscountUsages_OrderId_DiscountId",
                table: "DiscountUsages");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_DiscountName",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_DiscountProducts_DiscountId_ProductId",
                table: "DiscountProducts");

            migrationBuilder.DropIndex(
                name: "IX_CouponUsages_CouponUsageOrderId_CouponUsageCouponId",
                table: "CouponUsages");

            migrationBuilder.DropIndex(
                name: "IX_CouponProductTypes_CouponId_ProductTypeId",
                table: "CouponProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_CouponProducts_CouponId_ProductId",
                table: "CouponProducts");

            migrationBuilder.DropIndex(
                name: "IX_CouponCategories_CouponId_CategoryId",
                table: "CouponCategories");

            migrationBuilder.DropIndex(
                name: "IX_CouponBrands_CouponId_BrandId",
                table: "CouponBrands");

            migrationBuilder.DropIndex(
                name: "IX_CommentVotes_CommentVoteUserId_CommentVoteCommentId",
                table: "CommentVotes");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandName",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDefinitions_Name",
                table: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerVotes_AnswerVoteUserId_AnswerVoteAnswerId",
                table: "AnswerVotes");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_Description",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId_AddressName",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "StatusNameString",
                table: "StatusNames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductPrice",
                table: "Products",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CommentText",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AttributeDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_AttributeDefinitionId",
                table: "ProductTypeAttributes",
                column: "AttributeDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_ProductRateUserId",
                table: "ProductRates",
                column: "ProductRateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeDefinitionId",
                table: "ProductAttributes",
                column: "AttributeDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedProducts_UserId",
                table: "FollowedProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProducts_DiscountId",
                table: "DiscountProducts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponProductTypes_CouponId",
                table: "CouponProductTypes",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponProducts_CouponId",
                table: "CouponProducts",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponCategories_CouponId",
                table: "CouponCategories",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponBrands_CouponId",
                table: "CouponBrands",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentVoteUserId",
                table: "CommentVotes",
                column: "CommentVoteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVotes_AnswerVoteUserId",
                table: "AnswerVotes",
                column: "AnswerVoteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");
        }
    }
}
