using Microsoft.Extensions.AI;

namespace DotNetAiErudio.Service
{
    public class ChatService
    {
        private readonly IChatClient _chatClient;

        public ChatService(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var request = new ChatOptions
            {
                MaxOutputTokens = 200,
                ModelId = "gpt-4o",
                Temperature = 0.4f
            };

            var response = await _chatClient.GetResponseAsync(prompt, request);
            return response.Messages.FirstOrDefault()?.Text ?? "No response from AI.";
        }

        public async Task<string> GetResponseWithOptionsAsync(string prompt)
        {
            var request = new ChatOptions
            {
                ModelId = "gpt-4o",
                Temperature = 0.4f,
                MaxOutputTokens = 200
            };

            var response = await _chatClient.GetResponseAsync(prompt, request);
            return response.Messages.FirstOrDefault()?.Text ?? "No response from AI."; // Fix the error here
        }
    }
}
