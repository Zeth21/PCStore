using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers
{
    public class RemoveAttributeDefinitionByIdHandler(ProjectDbContext context) : IRequestHandler<RemoveAttributeDefinitionByIdCommand, Result>
    {
        private readonly ProjectDbContext _context = context;

        public async Task<Result> Handle(RemoveAttributeDefinitionByIdCommand request, CancellationToken cancellationToken)
        {
            var attribute = await _context.AttributeDefinitions
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (attribute is null)
                return Result.NotFound(message: "No attribute definitons found!");
            try
            {
                _context.AttributeDefinitions.Remove(attribute);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(message: "Attribute definition removed successfully!");
            }
            catch (Exception ex)
            {
                return Result.Fail(message: "Process failed!" + ex.Message);
            }
        }
    }
}
