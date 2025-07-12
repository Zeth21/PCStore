using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentHandlers
{
    public class CreateCommentHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCommentCommand, TaskResult<CreateCommentResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskResult<CreateCommentResult>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users.FindAsync(request.CommentUserId,cancellationToken);
            if (checkUser is null) 
                return TaskResult<CreateCommentResult>.Fail("User not found!");

            var checkProduct = await _context.Products.FindAsync(request.CommentProductId,cancellationToken);
            if (checkProduct is null)
                return TaskResult<CreateCommentResult>.Fail("Product not found!");

            var comment = _mapper.Map<Comment>(request);

            try 
            {
                await _context.Comments.AddAsync(comment,cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateCommentResult>.Fail("Process has failed! : " + ex.Message);
            }

            var result = _mapper.Map<CreateCommentResult>(comment);
            return TaskResult<CreateCommentResult>.Success("Comment created successfully!", data: result);
        }
    }
}
