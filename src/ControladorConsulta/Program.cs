using ControladorConsulta.Database;
using ControladorConsulta.Models;
using ControladorConsulta.Repositories;
using ControladorConsulta.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


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

app.Run();

