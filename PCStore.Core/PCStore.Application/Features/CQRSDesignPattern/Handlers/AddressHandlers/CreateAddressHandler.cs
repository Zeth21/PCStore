using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AddressHandlers
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, TaskResult<CreateAddressResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public CreateAddressHandler(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskResult<CreateAddressResult>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var nameExists = await _context.Addresses
               .AnyAsync(x => x.AddressName == request.AddressName && x.UserId == request.UserId, cancellationToken);
                if (nameExists)
                    return TaskResult<CreateAddressResult>.Fail("The name is already taken!");
                var addressExists = await _context.Addresses
                    .AnyAsync(x => x.Description == request.Description && x.UserId == request.UserId);
                if (addressExists)
                    return TaskResult<CreateAddressResult>.Fail("You already have that address!");
                var newAddress = _mapper.Map<Address>(request);
                await _context.Addresses.AddAsync(newAddress, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<CreateAddressResult>(newAddress);
                return TaskResult<CreateAddressResult>.Success("Address has created successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateAddressResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
