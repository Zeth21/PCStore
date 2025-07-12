using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentHandlers
{
    public class UpdateCommentHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateCommentCommand, TaskResult<UpdateCommentResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskResult<UpdateCommentResult>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .Where(x=> x.CommentId == request.CommentId && x.CommentUserId == request.CommentUserId)
                .FirstOrDefaultAsync(cancellationToken);
            if (comment is null)
                return TaskResult<UpdateCommentResult>.NotFound("No comments found!");
            if (comment.CommentText == request.CommentText)
                return TaskResult<UpdateCommentResult>.Fail("No changes detected!");
            var checkTimeLimit = (DateTime.Now - comment.CommentDate).Duration();
            if (checkTimeLimit >= TimeSpan.FromMinutes(15))
                return TaskResult<UpdateCommentResult>.Fail("Time limit has exceeded!");
            var answers = await _context.Answers
                .Where(x => x.AnswerCommentId == comment.CommentId).ToListAsync(cancellationToken);
            if(answers.Count > 0)
                return TaskResult<UpdateCommentResult>.Fail("Comment has an answer!");
            comment.CommentText = request.CommentText;
            comment.CommentDate = DateTime.Now;
            try 
            {
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<UpdateCommentResult>(comment);
                return TaskResult<UpdateCommentResult>.Success("Comment has updated successfully!", data : result);
            }
            catch(Exception ex) 
            {
                return TaskResult<UpdateCommentResult>.Fail("Process has failed! " + ex.Message);
            }
        }
    }
}
