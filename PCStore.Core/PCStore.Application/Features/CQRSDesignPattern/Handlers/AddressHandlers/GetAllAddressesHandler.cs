using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AddressQueries;
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
    public class GetAllAddressesHandler : IRequestHandler<GetAllAddressesQuery, TaskListResult<GetAllAddressesResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public GetAllAddressesHandler(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskListResult<GetAllAddressesResult>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var addresses = await _context.Addresses
                    .Where(x => x.UserId == request.UserId)
                    .AsNoTracking()
                    .ProjectTo<GetAllAddressesResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                if (addresses.Count <= 0)
                    return TaskListResult<GetAllAddressesResult>.NotFound("No address has found!");
                return TaskListResult<GetAllAddressesResult>.Success("All addresses has found successfully!", addresses);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetAllAddressesResult>.NotFound("Something went wrong! " + ex.Message);
            }
        }
    }
}
