using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.UserHandlers
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, TaskResult<GetUserProfileResult>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetUserProfileHandler(IMapper mapper, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<TaskResult<GetUserProfileResult>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                    return TaskResult<GetUserProfileResult>.Fail("Invalid user!");
                var result = _mapper.Map<GetUserProfileResult>(user);
                return TaskResult<GetUserProfileResult>.Success("Profile found successfully!",result);
            }
            catch(Exception ex) 
            {
                return TaskResult<GetUserProfileResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
