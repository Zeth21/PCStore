namespace PCStore.UI.Models.Results.AddressResults
{
    public class GetAllAddressesResult
    {
        public int Id { get; set; }
        public required string AddressName { get; set; }
        public required string Description { get; set; }
    }

}
