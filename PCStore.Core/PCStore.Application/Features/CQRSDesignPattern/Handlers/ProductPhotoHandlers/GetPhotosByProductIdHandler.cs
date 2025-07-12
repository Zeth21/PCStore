using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductPhotoQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductPhotoHandlers
{
    public class GetPhotosByProductIdHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<GetPhotosByProductIdQuery, TaskListResult<GetPhotosByProductIdResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskListResult<GetPhotosByProductIdResult>> Handle(GetPhotosByProductIdQuery request, CancellationToken cancellationToken)
        {
            var photos = await _projectDbContext.ProductPhotos
                .Where(x => x.PhotoProductId == request.ProductId)
                .ToListAsync(cancellationToken);
            if (photos.Count == 0)
                return TaskListResult<GetPhotosByProductIdResult>.NotFound("Photos not found!");
            var result = _mapper.Map<List<GetPhotosByProductIdResult>>(photos);
            return TaskListResult<GetPhotosByProductIdResult>.Success(message: "All photos are found!", data: result);
        }
    }
}
