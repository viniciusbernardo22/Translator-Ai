using Translator_Ai.Infraestructure.Configurations.Translator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureTranslator(builder.Configuration);

builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables()
       .AddUserSecrets<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
