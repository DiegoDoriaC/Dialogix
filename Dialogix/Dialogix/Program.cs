using Dialogix.Application.Features.Interfaces;
using Dialogix.Application.Features.Services;
using Dialogix.ChatBot;
using Dialogix.ChatBot.Interfaces;
using Dialogix.Infrastructure.Database;
using Dialogix.Infrastructure.Repositories;
using Essalud.Application.Feature.Interfaces;
using Essalud.Application.Feature.Services;
using Essalud.Infraestructure.Database;
using Essalud.Infraestructure.Repositories;
using Essalud.Infraestructure.Repositories.Interfaces;

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
string cadenaEssalud = builder.Configuration.GetConnectionString("CadenaEssalud")!;
builder.Services.AddSingleton<ISqlConnectionEssaludFactory>(new SqlConnectionEssaludFactory(cadenaEssalud));

// Configuracion de la inyecion de dependencias
builder.Services.AddScoped<IChatbotService, ChatbotService>();
builder.Services.AddScoped<IConversacionService, ConversacionService>();
builder.Services.AddScoped<IMedicosRepository, MedicosRepository>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<InteraccionChatbot>();

builder.Services.AddScoped<IFlujoAgendarCita, FlujoAgendarCita>();
builder.Services.AddScoped<IFlujoCancelarCita, FlujoCancelarCita>();
builder.Services.AddScoped<IFlujoConsultarResultados, FlujoConsultarResultados>();
builder.Services.AddScoped<IFlujoHistorialCitas, FlujoHistorialCitas>();

builder.Services.AddScoped<IPreguntasFrecuentesService, PreguntasFrecuentesService>();
builder.Services.AddScoped<IPreguntasFrecuentesRepository, PreguntasFrencuenteRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IReportesService, ReportesService>();
builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
builder.Services.AddScoped<IReportesCitasRepository, ReportesCitasRepository>();


builder.Services.AddScoped<IPacientesService, PacientesService>();
builder.Services.AddScoped<IPacientesRepository, PacientesRepository>();

builder.Services.AddScoped<IHorariosMedicoService, HorariosMedicoService>();
builder.Services.AddScoped<IHorarioMedicoRepository, HorarioMedicoRepository>();

builder.Services.AddScoped<ICitasMedicasService, CitaMedicaService>();
builder.Services.AddScoped<ICitasMedicasRepository, CitasMedicasRepository>();

builder.Services.AddScoped<IResultadosService, ResultadosService>();
builder.Services.AddScoped<IResultadosRepository, ResultadosRepository>();

// Habilitar el acceso a la session
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod()

            .AllowCredentials();
    });
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseSession();              
app.UseAuthorization();
app.MapControllers();
app.Run();
