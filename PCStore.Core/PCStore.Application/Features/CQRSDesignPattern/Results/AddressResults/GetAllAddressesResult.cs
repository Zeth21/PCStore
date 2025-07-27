using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults
{
    public class GetAllAddressesResult
    {
        public int Id { get; set; }
        public required string AddressName { get; set; }
        public required string Description { get; set; }
    }
}
