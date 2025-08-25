using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Services.CommentService;
using System.Security.Claims;

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

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int id, CancellationToken cancellation = default) 
        {
            var request = new RemoveCommentCommand { CommentId = id };
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await _commentService.RemoveComment(request,cancellation);
            return StatusCode(result.StatusCode,result.Message);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand request,CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.CommentUserId = userId;
            var result = await _commentService.CreateComment(request,cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand request, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.CommentUserId = userId;
            var result = await _commentService.UpdateComment(request, cancellation);
            return StatusCode(result.StatusCode,result);
        }
    }
}
