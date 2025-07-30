namespace PCStore.UI.Models.Queries.ProductQueries
{
    public class GetAllProductsQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

    }
}
