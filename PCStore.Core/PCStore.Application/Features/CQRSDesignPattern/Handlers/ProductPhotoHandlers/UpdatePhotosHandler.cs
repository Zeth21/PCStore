using AutoMapper;
using Bogus.Bson;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Persistence.Context;


namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductPhotoHandlers
{
    public class UpdatePhotosHandler(ProjectDbContext context,IMapper mapper) : IRequestHandler<UpdatePhotosCommand, TaskListResult<UpdatePhotosResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskListResult<UpdatePhotosResult>> Handle(UpdatePhotosCommand request, CancellationToken cancellationToken)
        {
            var reqIds = request.Values.Select(x => x.PhotoId).ToList();
            var checkProduct = await _context.Products
                .Where(x => x.ProductId == request.ProductId)
                .AnyAsync(cancellationToken);
            if (!checkProduct)
                return TaskListResult<UpdatePhotosResult>.NotFound("Product not found!");
            var dbPhotos = await _context.ProductPhotos
                .Where(x => x.PhotoProductId == request.ProductId && reqIds.Contains(x.PhotoId))
                .ToListAsync(cancellationToken);
            if (dbPhotos.Select(x => x.PhotoId).ToList().Count != reqIds.Count)
                return TaskListResult<UpdatePhotosResult>.Fail("One or more photos does not belong to this product!");
            var rootPath = request.WebRootPath;
            var oldPathList = dbPhotos.Select(x => x.PhotoPath);
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken)) 
            {
                try
                {
                    for (int i = 0; i < dbPhotos.Count; i++)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Values[i].Photo.FileName)}";
                        var fullPath = Path.Combine(rootPath!, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await request.Values[i].Photo.CopyToAsync(stream, cancellationToken);
                        }
                        dbPhotos.Where(x => x.PhotoId == request.Values[i].PhotoId).First().PhotoName = request.Values[i].Photo.FileName;
                        dbPhotos.Where(x => x.PhotoId == request.Values[i].PhotoId).First().PhotoPath = fileName;
                    }
                    var task = await _context.SaveChangesAsync(cancellationToken);
                    if (task <= 0)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return TaskListResult<UpdatePhotosResult>.Fail("Something went wrong when saving the data!");
                    }
                    await transaction.CommitAsync(cancellationToken);
                    foreach (var path in oldPathList)
                    {
                        var fullPath = Path.Combine(rootPath!, path);
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    }
                    var result = _mapper.Map<List<UpdatePhotosResult>>(dbPhotos);
                    return TaskListResult<UpdatePhotosResult>.Success("All photos updated successfully!", result);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return TaskListResult<UpdatePhotosResult>.Fail("Something went wrong! " + ex.Message);
                }

            }

        }
    }
}
