using OpenAI;

namespace DotNetAiErudio_VoiceTranscription.Extensions
{
    public static class OpenAIExtensions
    {
        public static WebApplicationBuilder AddOpenAI(this WebApplicationBuilder builder)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            var openAIClient = new OpenAIClient(apiKey);

            builder.Services.AddSingleton(openAIClient);
            return builder;
        }
    }
}
