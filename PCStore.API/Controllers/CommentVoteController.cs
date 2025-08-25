using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Services.CommentVoteService;
using System.Security.Claims;

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.CommentVoteUserId = userId;
            var result = await _commentVoteService.CreateCommentVote(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveCommentVote([FromBody] RemoveCommentVoteCommand request, CancellationToken cancellation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await _commentVoteService.RemoveCommentVote(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
