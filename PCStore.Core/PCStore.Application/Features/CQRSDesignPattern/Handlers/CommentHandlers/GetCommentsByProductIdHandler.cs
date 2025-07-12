using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentHandlers
{
    public class GetCommentsByProductIdHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<GetCommentsByProductIdQuery, TaskListResult<GetCommentsByProductIdResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;


        public async Task<TaskListResult<GetCommentsByProductIdResult>> Handle(GetCommentsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;

            try
            {
                var comments = await _projectDbContext.Comments
                    .Include(x => x.User)
                    .Where(x => !x.CommentIsQuestion && x.CommentProductId == request.CommentProductId)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ProjectTo<GetCommentsByProductIdResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                if (comments.Count == 0)
                    return TaskListResult<GetCommentsByProductIdResult>.NotFound(message: "No comments found!");
                return TaskListResult<GetCommentsByProductIdResult>.Success(message: "All comments found successfully!", data: comments);
            }
            catch (Exception ex)
            {
                return TaskListResult<GetCommentsByProductIdResult>.Fail(message: "Process failed!", errors: [ex.Message]);
            }
        }
    }
}
