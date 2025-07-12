using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductTypeAttributeHandlers
{
    class RemoveTypeAttributeHandler(ProjectDbContext context) : IRequestHandler<RemoveTypeAttributeCommand, Result>
    {
        private readonly ProjectDbContext _context = context;

        public async Task<Result> Handle(RemoveTypeAttributeCommand request, CancellationToken cancellationToken)
        {
            var checkAttr = await _context.ProductTypeAttributes
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkAttr is null)
                return Result.Fail(message: "Type attribute not found!");
            try
            {
                _context.ProductTypeAttributes.Remove(checkAttr);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(message: "Process failed! " + ex.Message);
            }
        }
    }
}
