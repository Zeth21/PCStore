

namespace PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults
{
    public class CreateAddressResult
    {
        public int Id { get; set; }
        public required string AddressName { get; set; }
        public required string Description { get; set; }
    }
}
