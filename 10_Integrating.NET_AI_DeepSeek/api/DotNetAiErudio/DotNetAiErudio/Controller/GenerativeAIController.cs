using DotNetAiErudio.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAiErudio.Controller
{
    [ApiController]
    [Route("ai")]
    public class GenerativeAIController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly RecipeService _recipeService;
        private readonly ImageService _imageService;

        public GenerativeAIController(
            ChatService chatService,
            RecipeService recipeService,
            ImageService imageService
        )
        {
            _chatService = chatService;
            _recipeService = recipeService;
            _imageService = imageService;
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

        [HttpGet("generate-image")]
        public async Task<IActionResult> GenerateImage(
            [FromQuery] string prompt,
            [FromQuery] string quality = "hd",
            [FromQuery] int n = 1,
            [FromQuery] int height = 1024,
            [FromQuery] int width = 1024)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("The 'prompt' parameter is required and cannot be empty.");
            }
            var imageUrls = await _imageService.GenerateImage(prompt, quality, n, height, width);
            return Ok(imageUrls);
        }
    }
}
