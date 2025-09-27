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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins(
                "https://localhost:4200",
                "https://frontend-translator-ai.vercel.app",
                "https://translator-ai-white-resonance-7456.fly.dev"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("AllowAngularDev");
app.MapControllers();

app.Run();
