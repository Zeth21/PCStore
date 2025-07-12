using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductPhotoHandlers
{
    public class CreateProductPhotosHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateProductPhotoCommand, TaskListResult<GetPhotosByProductIdResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskListResult<GetPhotosByProductIdResult>> Handle(CreateProductPhotoCommand request, CancellationToken cancellationToken)
        {
            var checkProduct = await _context.Products
                .Where(x => x.ProductId == request.PhotoProductId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkProduct is null || request.Photos.Count <= 0)
                return TaskListResult<GetPhotosByProductIdResult>.Fail(message: "Product not found!");

            var folderPath = Path.Combine(request.WebRootPath!, "product-photos");

            try
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var photoList = new List<ProductPhoto>();
                foreach (var photo in request.Photos)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream, cancellationToken);
                    }
                    photoList.Add(new ProductPhoto
                    {
                        PhotoProductId = request.PhotoProductId,
                        PhotoName = photo.FileName,
                        PhotoPath = fileName
                    });
                    if (photo == request.Photos[0])
                        checkProduct.ProductMainPhotoPath = fileName;
                }
                await _context.ProductPhotos.AddRangeAsync(photoList, cancellationToken);
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return TaskListResult<GetPhotosByProductIdResult>.Fail(message: "Failed to save to the database!");
                var result = _mapper.Map<List<GetPhotosByProductIdResult>>(photoList);
                return TaskListResult<GetPhotosByProductIdResult>.Success(message: "Photos created successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskListResult<GetPhotosByProductIdResult>.Fail(message: "Process has failed! " + ex.Message);
            }
        }
    }
}
