namespace PCStore.UI.Services
{
    // PCStore.UI/Services/ProductService.cs
    using System.Net.Http.Json;
    using PCStore.UI.Models.Queries.ProductQueries;
    using PCStore.UI.Models.Results.ProductResults;

    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<GetFacadeProductDetailsResult?> GetProductDetailsAsync(int productId)
        {
            var response = await _http.GetAsync($"api/Product/{productId}");
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<GetFacadeProductDetailsResult>();
            return result;
        }

        public async Task<List<ProductCardModel>> GetAllProductsAsync(GetAllProductsQuery query)
        {
            var url = $"api/Product?pageNumber={query.PageNumber}&pageSize={query.PageSize}";

            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(query.Name)) queryParams.Add($"name={query.Name}");
            if (!string.IsNullOrEmpty(query.CategoryName)) queryParams.Add($"categoryName={query.CategoryName}");
            if (query.MinPrice.HasValue) queryParams.Add($"minPrice={query.MinPrice}");
            if (query.MaxPrice.HasValue) queryParams.Add($"maxPrice={query.MaxPrice}");


            if (queryParams.Any())
            {
                url += "&" + string.Join("&", queryParams);
            }

            return await _http.GetFromJsonAsync<List<ProductCardModel>>(url) ?? new();
        }
    }

}
