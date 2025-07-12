using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentHandlers
{
    public class RemoveCommentHandler(ProjectDbContext context) : IRequestHandler<RemoveCommentCommand, Result>
    {
        private readonly ProjectDbContext _context = context;

        async Task<Result> IRequestHandler<RemoveCommentCommand, Result>.Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var checkComment = await _context.Comments
                .Where(x => x.CommentUserId == request.UserId && x.CommentId == request.CommentId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkComment is null)
                return Result.Fail("Permission denied!");
            try 
            {
                _context.Remove(checkComment);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("Comment has removed successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Transaction has failed! : " + ex.Message);
            }
        }
    }
}
