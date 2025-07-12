using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries;
using PCStore.Application.Services.AnswerService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController(IAnswerService answerService) : ControllerBase
    {
        private readonly IAnswerService _answerService = answerService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswers([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetAnswersByCommentIdQuery { CommentId = id };
            var result = await _answerService.GetAnswers(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new RemoveAnswerByIdCommand { AnswerId = id };
            var result = await _answerService.RemoveAnswer(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Message);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = await _answerService.CreateAnswer(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateAnswer([FromBody] UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = await _answerService.UpdateAnswer(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
