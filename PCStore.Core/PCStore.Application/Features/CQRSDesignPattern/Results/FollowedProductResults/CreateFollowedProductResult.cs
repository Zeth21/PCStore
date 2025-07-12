using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults
{
    public class CreateFollowedProductResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int? ProductStock { get; set; }

    }
}
