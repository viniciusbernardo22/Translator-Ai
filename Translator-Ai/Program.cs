using Translator_Ai.Infraestructure.Configurations.Translator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureTranslator(builder.Configuration);

var app = builder.Build();
var isProd = app.Environment.IsDevelopment();

if (!isProd)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
