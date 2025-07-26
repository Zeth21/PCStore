using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults
{
    public class UpdateBrandResult
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
    }
}
