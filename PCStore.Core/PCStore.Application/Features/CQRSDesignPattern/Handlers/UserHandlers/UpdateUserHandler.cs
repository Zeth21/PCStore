using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.UserHandlers
{
    public class UpdateUserHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateUserCommand, TaskResult<UpdateUserResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskResult<UpdateUserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users.FindAsync(request.UserId,cancellationToken);
            if (checkUser is null)
                return TaskResult<UpdateUserResult>.NotFound("User not found!");
            foreach(var prop in request.GetType().GetProperties()) 
            {
                if (prop.Name == "UserId")
                    continue;
                var value = prop.GetValue(request) as string;
                if (!string.IsNullOrEmpty(value)) 
                {
                    var userProp = checkUser.GetType().GetProperty(prop.Name);
                    if (userProp is not null)
                    {
                        userProp.SetValue(checkUser, value);
                        continue;
                    }
                    return TaskResult<UpdateUserResult>.Fail("Unknown property detected!");
                }
            }
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<UpdateUserResult>(checkUser);
                return TaskResult<UpdateUserResult>.Success("User updated successfully!", data : result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateUserResult>.Fail("Update command has failed : " + ex.Message);
            }
        }
    }
}
