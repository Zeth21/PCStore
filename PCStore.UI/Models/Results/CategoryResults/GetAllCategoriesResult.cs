namespace PCStore.UI.Models.Results.CategoryResults
{
    public class GetAllCategoriesResult
    {
        public int? ParentCategoryId { get; set; }
        public List<GetCategoryDTO> Categories { get; set; } = [];
    }
    public class GetCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<GetCategoryDTO>? SubCategories { get; set; }
    }
}
