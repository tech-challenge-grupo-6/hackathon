using ControladorConsulta.Database;
using ControladorConsulta.Models;
using ControladorConsulta.Repositories;
using ControladorConsulta.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Controlador Consta", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddDbContext<DatabaseContext>(opitions => opitions.UseInMemoryDatabase("ControladorConsulta"));
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context
        => await Results.Problem()
                     .ExecuteAsync(context)));

var medico = app.MapGroup("medico")
    .WithTags("Medico")
    .WithOpenApi();

medico.MapGet("/{id}", async (IMedicoService medicoService, Guid Id) =>
{
    var medicoOutput = await medicoService.ObterPorIdAsync(Id);
    return medicoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(medicoOutput)
    };
})
.WithName("Obter Medico")
.WithOpenApi();

medico.MapPost("/", async (IMedicoService medicoService, MedicoInput medicoInput) =>
{
    Guid id = await medicoService.InserirAsync(medicoInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Medico")
.WithOpenApi();

medico.MapPut("/{id}", async (IMedicoService medicoService, Guid Id, MedicoInput medicoInput) =>
{
    var medicoOutput = await medicoService.AtualizarAsync(Id, medicoInput);
    return medicoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(medicoOutput)
    };
})
.WithName("Atualizar Medico")
.WithOpenApi();

medico.MapDelete("/{id}", async (IMedicoService medicoService, Guid Id) =>
{
    var medicoOutput = await medicoService.RemoverAsync(Id);
    return medicoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(medicoOutput)
    };
})
.WithName("Remover Medico")
.WithOpenApi();

var paciente = app.MapGroup("paciente")
    .WithTags("Paciente")
    .WithOpenApi();

paciente.MapGet("/{id}", async (IPacienteService pacienteService, Guid Id) =>
{
    var pacienteOutput = await pacienteService.ObterPorIdAsync(Id);
    return pacienteOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(pacienteOutput)
    };
})
.WithName("Obter Paciente")
.WithOpenApi();

paciente.MapPost("/", async (IPacienteService pacienteService, PacienteInput pacienteInput) =>
{
    Guid id = await pacienteService.InserirAsync(pacienteInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Paciente")
.WithOpenApi();

paciente.MapPut("/{id}", async (IPacienteService pacienteService, Guid Id, PacienteInput pacienteInput) =>
{
    var pacienteOutput = await pacienteService.AtualizarAsync(Id, pacienteInput);
    return pacienteOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(pacienteOutput)
    };
})
.WithName("Atualizar Paciente")
.WithOpenApi();

paciente.MapDelete("/{id}", async (IPacienteService pacienteService, Guid Id) =>
{
    var pacienteOutput = await pacienteService.RemoverAsync(Id);
    return pacienteOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(pacienteOutput)
    };
})
.WithName("Remover Paciente")
.WithOpenApi();

app.Run();

