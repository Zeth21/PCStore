using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Shared.DTOs.Get
{
    public class GetProductsDTO
    {
        public string ProductName { get; set; }
        public short ProductStock { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal ProductPrice { get; set; }
    }
}
