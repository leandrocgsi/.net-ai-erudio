using OpenAI;
using OpenAI.Audio;

namespace DotNetAiErudio_VoiceTranscription.Service
{
    public class TranscriptionService
    {

        private readonly OpenAIClient _openAIClient;
        private readonly string _model;

        public TranscriptionService(OpenAIClient openAIClient, IConfiguration configuration)
        {
            _openAIClient = openAIClient;
            _model = configuration["OpenAi:AudioModel"] ?? "whisper-1";
        }

        public async Task<string> TranscribeAudioAsync(IFormFile file)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");

            try
            {
                var stream = new FileStream(tempFilePath, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();

                var options = new AudioTranscriptionOptions
                {
                    ResponseFormat = AudioTranscriptionFormat.Text,
                    Language = "pt",
                    Temperature = 0.0f,
                };

                var audioClient = _openAIClient.GetAudioClient(_model);

                var response = await audioClient.TranscribeAudioAsync(tempFilePath, options);

                return response.Value.Text ?? "No transcription generated!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
