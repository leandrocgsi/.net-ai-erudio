using DotNetAiErudio.Extensions;
using DotNetAiErudio.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddOpenAI();

// Registra o ChatService como singleton
builder.Services.AddScoped<ChatService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
