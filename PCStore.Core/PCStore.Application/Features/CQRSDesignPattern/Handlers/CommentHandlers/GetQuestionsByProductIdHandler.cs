using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentHandlers
{
    public class GetQuestionsByProductIdHandler(ProjectDbContext projectDbContext) : IRequestHandler<GetQuestionsByProductIdQuery, TaskListResult<GetQuestionsByProductIdResult>>
    {
        private readonly ProjectDbContext _context = projectDbContext;
        async Task<TaskListResult<GetQuestionsByProductIdResult>> IRequestHandler<GetQuestionsByProductIdQuery, TaskListResult<GetQuestionsByProductIdResult>>.Handle(GetQuestionsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var pagesize = request.PageSize;
            var pageindex = request.PageIndex;
            var checkQuestions = await _context.Comments
                .Include(x => x.User)
                .Include(x => x.Answers)
                .Where(x => x.CommentIsQuestion && x.CommentProductId == request.ProductId)
                .OrderByDescending(x => x.CommentDate)
                .Skip(pagesize * pageindex) // Sayfaya göre atlama 
                .Take(pagesize) // Kaç tane almak istiyosan, Default = 4
                .Select(x => new GetQuestionsByProductIdResult
                {
                    QuestionId = x.CommentId,
                    QuestionText = x.CommentText,
                    QuestionDate = x.CommentDate,
                    QuestionUpVoteCount = x.CommentUpVoteCount,
                    UserName = x.User != null ? x.User.UserName : "Bilinmeyen Kullanıcı",
                    Answers = x.Answers.Take(1).Select(a => new GetAnswersByQuestionIdResult  //Take 1 sebebi sadece satıcı yanıtı var.
                    {
                        Id = a.AnswerId,
                        Date = a.AnswerDate,
                        Text = a.AnswerText,
                        UserName = a.User != null ? a.User.UserName : "Bilinmeyen Kullanıcı"
                    }).ToList()
                }).ToListAsync(cancellationToken);

            if (checkQuestions.Count == 0)
            {
                return TaskListResult<GetQuestionsByProductIdResult>.NotFound(message: "No questions found!");
            }
            return TaskListResult<GetQuestionsByProductIdResult>.Success(message: "All questions found successfully!", data: checkQuestions);

        }
    }
}

