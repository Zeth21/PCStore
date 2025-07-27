using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AddressHandlers
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, TaskResult<UpdateAddressResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAddressHandler(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskResult<UpdateAddressResult>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var address = await _context.Addresses
                    .SingleOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id, cancellationToken);
                if (address is null)
                    return TaskResult<UpdateAddressResult>.Fail("Address not found!");
                if (!string.IsNullOrEmpty(request.AddressName))
                {
                    var nameExists = await _context.Addresses
                        .AnyAsync(x => x.AddressName == request.AddressName && x.Id != request.Id, cancellationToken);
                    if (nameExists)
                        return TaskResult<UpdateAddressResult>.Fail("Name is already taken!");
                    address.AddressName = request.AddressName;
                }
                if (!string.IsNullOrEmpty(request.Description))
                {
                    var addressExists = await _context.Addresses
                        .AnyAsync(x => x.Description == request.Description && x.UserId == request.UserId, cancellationToken);
                    if (addressExists)
                        return TaskResult<UpdateAddressResult>.Fail("Another address already uses this description!");
                    address.Description = request.Description;
                }
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<UpdateAddressResult>(address);
                return TaskResult<UpdateAddressResult>.Success("Address has updated successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<UpdateAddressResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
