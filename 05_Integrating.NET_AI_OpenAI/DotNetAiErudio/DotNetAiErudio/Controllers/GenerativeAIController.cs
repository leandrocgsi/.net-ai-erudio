using DotNetAiErudio.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetAiErudio.Controllers
{
    [ApiController]
    [Route("ai")]
    public class GenerativeAIController : ControllerBase
    {
        private readonly ChatService _chatService;

        public GenerativeAIController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("ask-ai")]
        public async Task<IActionResult> GetResponse([FromQuery] string prompt)
        {
            var response = await _chatService.GetResponseAsync(prompt);
            return Ok(response);
        }

        [HttpGet("ask-ai-options")]
        public async Task<IActionResult> GetResponseWithOptions([FromQuery] string prompt)
        {
            var response = await _chatService.GetResponseWithOptionsAsync(prompt);
            return Ok(response);
        }
    }
}
