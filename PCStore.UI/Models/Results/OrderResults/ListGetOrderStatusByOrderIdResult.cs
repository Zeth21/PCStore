namespace PCStore.UI.Models.Results.OrderResults
{
    public class ListGetOrderStatusByOrderIdResult
    {
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public required string StatusName { get; set; }
    }
}
