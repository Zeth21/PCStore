using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductPhotoHandlers
{
    public class RemoveProductPhotoHandler(ProjectDbContext context) : IRequestHandler<RemoveProductPhotoCommand, Result>
    {
        private readonly ProjectDbContext _context = context;
        public async Task<Result> Handle(RemoveProductPhotoCommand request, CancellationToken cancellationToken)
        {
            var rootPath = request.WebRootPath;
            var checkPhotos = await _context.ProductPhotos
                .Where(x => x.PhotoProductId == request.ProductId)
                .ToListAsync(cancellationToken);
            if (checkPhotos.Count == 0)
                return Result.Fail("No photos found!");
            var dbIdList = checkPhotos.Select(x => x.PhotoId).ToList();
            var dbPhotoIds = new HashSet<int>(dbIdList);
            var removePhotoIds = new HashSet<int>(request.PhotoIds);
            if (!removePhotoIds.IsSubsetOf(dbPhotoIds))
                return Result.Fail("One or more photo does not belong to the product!");
            var removePhotos = checkPhotos.Where(x => request.PhotoIds.Contains(x.PhotoId)).ToList();
            try 
            {
                _context.RemoveRange(removePhotos);
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return Result.Fail("Something went wrong!");
                foreach(var photo in removePhotos) 
                {
                    var filePath = Path.Combine(rootPath!, photo.PhotoPath);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                return Result.Success("Photos are removed successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong!" + ex.Message);
            }
        }
    }
}
