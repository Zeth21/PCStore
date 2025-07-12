using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults
{
    public class UpdatePhotosResult
    {
        public int PhotoId { get; set; }
        public required string PhotoName { get; set; }
        public required string PhotoPath { get; set; }
    }
}
