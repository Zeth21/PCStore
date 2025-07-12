namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults
{
    public class CreateProductResult
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
    }
}
