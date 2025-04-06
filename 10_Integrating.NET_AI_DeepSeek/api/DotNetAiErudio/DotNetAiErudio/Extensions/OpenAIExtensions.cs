using OpenAI;
using System.ClientModel;

namespace DotNetAiErudio.Extensions
{
    public static class OpenAIExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder)
        {
            var apiKey = Environment.GetEnvironmentVariable("DEEP_SEEK_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            var option = new OpenAIClientOptions
            {
                Endpoint = new Uri("https://api.deepseek.com"),
            };

            var openAIClient = new OpenAIClient(new ApiKeyCredential(apiKey), option);

            builder.Services.AddSingleton(openAIClient);
            return builder;
        }
    }
}
