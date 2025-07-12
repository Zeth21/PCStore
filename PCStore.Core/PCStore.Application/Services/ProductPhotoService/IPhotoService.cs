using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.ProductPhotoService
{
    public interface IPhotoService
    {
        Task<Result> RemoveProductPhotos(RemoveProductPhotoCommand request, CancellationToken cancellationToken);
        Task<TaskListResult<UpdatePhotosResult>> UpdateProductPhotos(UpdatePhotosCommand request, CancellationToken cancellationToken);
    }
}
