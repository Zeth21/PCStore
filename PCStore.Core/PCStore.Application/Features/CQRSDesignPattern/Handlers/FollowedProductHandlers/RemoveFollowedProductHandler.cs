using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.FollowedProductHandlers
{
    public class RemoveFollowedProductHandler(ProjectDbContext context) : IRequestHandler<RemoveFollowedProductCommand,Result>
    {
        private readonly ProjectDbContext _context = context;

        public async Task<Result> Handle(RemoveFollowedProductCommand request, CancellationToken cancellationToken)
        {
            var checkFollowing = await _context.FollowedProducts
                .Where(x => x.UserId == request.UserId && x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkFollowing is null)
                return Result.NotFound("Following not found!");
            try 
            {
                _context.Remove(checkFollowing);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }

            return Result.Success("Product unfollowed successfully!");
        }
    }
}
