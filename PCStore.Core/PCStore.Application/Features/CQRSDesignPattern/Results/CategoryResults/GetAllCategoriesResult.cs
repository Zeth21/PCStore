using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults
{
    public class GetAllCategoriesResult
    {
        public int? ParentCategoryId { get; set; }
        public List<CategoryListItem> Categories { get; set; } = [];
    }

    public class CategoryListItem 
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
