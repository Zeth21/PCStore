using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Services.AnswerVoteService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerVoteController(IAnswerVoteService answerVoteService) : ControllerBase
    {
        private readonly IAnswerVoteService _answerVoteService = answerVoteService;

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAnswerVote([FromBody] CreateAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _answerVoteService.CreateAnswerVote(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAnswerVote([FromBody] RemoveAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _answerVoteService.RemoveAnswerVote(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
