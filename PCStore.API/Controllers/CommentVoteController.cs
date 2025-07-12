using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Services.CommentVoteService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentVoteController(ICommentVoteService commentVoteService) : ControllerBase
    {
        private readonly ICommentVoteService _commentVoteService = commentVoteService;

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCommentVote([FromBody] CreateCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _commentVoteService.CreateCommentVote(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveCommentVote([FromBody] RemoveCommentVoteCommand request, CancellationToken cancellation)
        {
            var result = await _commentVoteService.RemoveCommentVote(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
