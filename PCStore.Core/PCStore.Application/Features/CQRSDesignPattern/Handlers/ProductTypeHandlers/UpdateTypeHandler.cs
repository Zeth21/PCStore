using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductTypeHandlers
{
    public class UpdateTypeHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<UpdateTypeCommand, TaskResult<UpdateTypeResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;

        public async Task<TaskResult<UpdateTypeResult>> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            var checkType = await _context.ProductTypes
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkType is null || request.Name == null || checkType.Name == request.Name)
                return TaskResult<UpdateTypeResult>.NotFound(message: "Type not found!");
            try
            {
                checkType.Name = request.Name;
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<UpdateTypeResult>(checkType);
                return TaskResult<UpdateTypeResult>.Success(message: "Type updated successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateTypeResult>.Fail(message: "Process has failed! " + ex.Message);
            }
        }
    }
}
