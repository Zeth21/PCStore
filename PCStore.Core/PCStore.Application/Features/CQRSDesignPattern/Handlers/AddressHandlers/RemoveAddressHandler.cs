using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AddressHandlers
{
    public class RemoveAddressHandler : IRequestHandler<RemoveAddressCommand, Result>
    {
        private readonly ProjectDbContext _context;
        public RemoveAddressHandler(ProjectDbContext context) 
        {
            _context = context;
        }
        public async Task<Result> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var address = await _context.Addresses
               .SingleOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);
                if (address is null)
                    return Result.Fail("Address not found!");
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
