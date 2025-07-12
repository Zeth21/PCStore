using MediatR;
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
    public class PhotoService(IMediator mediator) : IPhotoService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<Result> RemoveProductPhotos(RemoveProductPhotoCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskListResult<UpdatePhotosResult>> UpdateProductPhotos(UpdatePhotosCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
