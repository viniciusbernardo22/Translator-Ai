using Translator_Ai.Infraestructure.Configurations.Translator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "https://localhost:4200",
                "https://frontend-translator-ai.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureTranslator(builder.Configuration);

var app = builder.Build();
var isDev = app.Environment.IsDevelopment();

if (isDev)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
