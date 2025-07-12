using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands
{
    public class UpdateProductAttributeCommand
    {
        public int Id { get; set; }
        public required string Value { get; set; }

    }
}
