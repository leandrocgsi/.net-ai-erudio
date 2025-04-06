using DotNetAiErudio.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAiErudio.Controller
{
    [ApiController]
    public class GenerativeAIController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly RecipeService _recipeService;

        public GenerativeAIController(
            ChatService chatService,
            RecipeService recipeService
        )
        {
            _chatService = chatService;
            _recipeService = recipeService;
        }

        [HttpGet("ask-ai")]
        public async Task<IActionResult> AskAi([FromQuery] string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("The 'prompt' parameter is required and cannot be empty.");
            }
            var response = await _chatService.GetResponseAsync(prompt);
            return Ok(response);
        }

        [HttpGet("ask-ai-options")]
        public async Task<IActionResult> AskAiWithOptions([FromQuery] string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("The 'prompt' parameter is required and cannot be empty.");
            }
            var response = await _chatService.GetResponseWithOptionsAsync(prompt);
            return Ok(response);
        }

        [HttpGet("recipe-creator")]
        public async Task<IActionResult> GenerateRecipe(
            [FromQuery] string ingredients,
            [FromQuery] string cuisine = "any",
            [FromQuery] string dietaryRestrictions = "none")
        {
            if (string.IsNullOrWhiteSpace(ingredients))
            {
                return BadRequest("The 'ingredients' parameter is required and cannot be empty.");
            }
            var recipe = await _recipeService.GenerateRecipe(ingredients, cuisine, dietaryRestrictions);
            return Ok(recipe);
        }
    }
}
