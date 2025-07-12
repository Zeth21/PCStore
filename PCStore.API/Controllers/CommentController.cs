using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Services.CommentService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsByProductId([FromRoute] int id, [FromQuery] int PageSize, [FromQuery] int pageIndex, CancellationToken cancellationToken)
        {
            var request = new GetCommentsByProductIdQuery { CommentProductId = id, PageSize = PageSize, PageIndex = pageIndex };
            var result = await _commentService.GetComments(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestionsByProductId([FromRoute] int id, [FromQuery] int PageSize, [FromQuery] int pageIndex, CancellationToken cancellationToken)
        {
            var request = new GetQuestionsByProductIdQuery { ProductId = id, PageSize = PageSize, PageIndex = pageIndex };
            var result = await _commentService.GetQuestions(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int id, [FromBody] string userId, CancellationToken cancellation = default) 
        {
            var request = new RemoveCommentCommand { CommentId = id, UserId = userId };
            var result = await _commentService.RemoveComment(request,cancellation);
            return StatusCode(result.StatusCode,result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand request,CancellationToken cancellation = default) 
        {
            var result = await _commentService.CreateComment(request,cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand request, CancellationToken cancellation = default) 
        {
            var result = await _commentService.UpdateComment(request, cancellation);
            return StatusCode(result.StatusCode,result);
        }
    }
}
