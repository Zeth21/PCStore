namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults
{
    public class GetPhotosByProductIdResult
    {
        public int PhotoId { get; set; }
        public string? PhotoPath { get; set; }
        public string? PhotoName { get; set; }
    }
}
