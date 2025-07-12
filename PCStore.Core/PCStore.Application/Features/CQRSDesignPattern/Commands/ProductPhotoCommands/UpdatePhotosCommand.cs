using MediatR;
using Microsoft.AspNetCore.Http;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands.SubClass;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands
{
    public class UpdatePhotosCommand : IRequest<TaskListResult<UpdatePhotosResult>>
    {
        public int ProductId { get; set; }
        public required List<UpdatePhotoValues> Values { get; set; }

        [JsonIgnore]
        public string? WebRootPath { get; set; }
    }
}
