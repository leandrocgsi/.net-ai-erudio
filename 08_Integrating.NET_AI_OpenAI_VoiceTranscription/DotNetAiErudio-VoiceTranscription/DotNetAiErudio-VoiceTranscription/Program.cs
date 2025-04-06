using DotNetAiErudio_VoiceTranscription.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.AddOpenAI();

// builder.Services.AddSingleton<ChatService>();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = ".NET AI Erudio API",
            Version = "v1",
            Description = """  
               This API provides AI-based features such as chat, image generation,  
               recipe creation and audio transcription.  
               """,
            Contact = new()
            {
                Name = "Erudio Training",
                Email = "contact@erudio.com.br",
                Url = new Uri("https://pub.erudio.com.br/meus-cursos")
            },
            License = new()
            {
                Name = "Apache 2 License",
                Url = new Uri("https://pub.erudio.com.br/meus-cursos")
            },
            TermsOfService = new Uri("https://pub.erudio.com.br/meus-cursos")
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = ".NET AI Erudio API";
        options.Theme = ScalarTheme.Default;
        options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
