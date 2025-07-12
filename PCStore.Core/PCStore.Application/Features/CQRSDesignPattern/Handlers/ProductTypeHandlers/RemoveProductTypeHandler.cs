using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductTypeHandlers
{
    public class RemoveProductTypeHandler(ProjectDbContext context) : IRequestHandler<RemoveProductTypeCommand, Result>
    {
        private readonly ProjectDbContext _context = context;

        public async Task<Result> Handle(RemoveProductTypeCommand request, CancellationToken cancellationToken)
        {
            var typeExists = await _context.ProductTypes
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (typeExists is null)
                return Result.NotFound(message: "No types has found!");
            try
            {
                _context.ProductTypes.Remove(typeExists);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(message: "Type has been removed successfully!");
            }
            catch (Exception ex)
            {
                return Result.Fail(message: "Process has failed! " + ex.Message);
            }
        }
    }
}
