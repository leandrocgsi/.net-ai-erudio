using OpenAI;

namespace DotNetAiErudio.Extensions
{
    public static class OpenAIExtensions
    {

        public static WebApplicationBuilder AddOpenAI(
            this WebApplicationBuilder builder)
        {
            // var key = builder.Configuration.GetValue<string>("OpenAI:Key");
            var key = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("OPEN_AI_API_KEY must be set.");
            }

            var chat = new OpenAIClient(key);
            builder.Services.AddSingleton(chat);
            return builder;
        }
    }
}
