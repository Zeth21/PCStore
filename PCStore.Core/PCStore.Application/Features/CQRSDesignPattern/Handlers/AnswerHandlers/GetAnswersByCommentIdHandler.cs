using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerHandlers
{
    public class GetAnswersByCommentIdHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<GetAnswersByCommentIdQuery, TaskListResult<GetAnswersByCommentIdResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskListResult<GetAnswersByCommentIdResult>> Handle(GetAnswersByCommentIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var answers = await _projectDbContext.Answers
                .Where(x => x.AnswerCommentId == request.CommentId)
                .ProjectTo<GetAnswersByCommentIdResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                if (answers.Count == 0)
                    return TaskListResult<GetAnswersByCommentIdResult>.NotFound(message: "No answers found!");
                return TaskListResult<GetAnswersByCommentIdResult>.Success(message: "Answers found successfully!", data: answers);
            }
            catch (Exception ex)
            {
                return TaskListResult<GetAnswersByCommentIdResult>.Fail(message: "Process failed!", errors: [ex.ToString()]);
            }
        }
    }
}
