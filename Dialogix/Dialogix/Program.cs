using Dialogix.Application.Features.Interfaces;
using Dialogix.Application.Features.Services;
using Dialogix.Infrastructure.Database;
using Dialogix.Infrastructure.Repositories;
using Essalud.Application.Feature.Interfaces;
using Essalud.Application.Feature.Services;
using Essalud.Infraestructure.Database;
using Essalud.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracion API KEY OpenAI
var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];

builder.Services.AddHttpClient<IOpenAIRepository, OpenAIRepository>(client =>
{
    client.BaseAddress = new Uri("https://api.groq.com/openai/v1/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");
});

// Configuracion Cadena SQL Dialogix
string cadenaDialogix = builder.Configuration.GetConnectionString("CadenaDialogix")!;
builder.Services.AddSingleton<ISqlConnectionDialogixFactory>(new SqlConnectionDialogixFactory(cadenaDialogix));

// Configuracion Cadena SQL Essalud
//string cadenaEssalud = builder.Configuration.GetConnectionString("CadenaEssalud")!;
//builder.Services.AddSingleton<ISqlConnectionEssaludFactory>(new SqlConnectionEssaludFactory(cadenaEssalud));

// Configuracion de la inyecion de dependencias
builder.Services.AddScoped<IChatbotService, ChatbotService>();
builder.Services.AddScoped<IPreguntasFrecuentesService, PreguntasFrecuentesService>();
builder.Services.AddScoped<IPreguntasFrecuentesRepository, PreguntasFrencuenteRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
