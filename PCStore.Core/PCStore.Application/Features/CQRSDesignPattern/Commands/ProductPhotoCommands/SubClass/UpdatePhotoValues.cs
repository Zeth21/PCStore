using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands.SubClass
{
    public class UpdatePhotoValues
    {
        public int PhotoId { get; set; }
        public required IFormFile Photo { get; set; }
    }
}
