using ControladorConsulta.Database;
using ControladorConsulta.Models;
using ControladorConsulta.Models.Dtos;
using ControladorConsulta.Models.Medicos;
using ControladorConsulta.Repositories;
using ControladorConsulta.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "dotnet-user-jwts",
            ValidAudience = "http://localhost:59096, https://localhost:44393, http://localhost:5005",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MDEyMzQ1Njc4OUFCQ0RFRjAxMjM0NTY3ODlBQkNERUY="))
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Controlador Consulta", Version = "v1" });
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
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<IAgendaService, AgendaService>();
builder.Services.AddScoped<IArquivoRepository, ArquivoRepository>();
builder.Services.AddScoped<IArquivoService, ArquivoService>();
builder.Services.AddScoped<IProntuarioRepository, ProntuarioRepository>();
builder.Services.AddScoped<IProntuarioService, ProntuarioService>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IDetalheConsultaRepository, DetalheConsultaRepository>();
builder.Services.AddScoped<IDetalheConsultaService, DetalheConsultaService>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context
        => await Results.Problem()
                     .ExecuteAsync(context)));

var detalhe = app.MapGroup("detalhe")
    .WithTags("Detalhe da consulta medica")
    .WithOpenApi()
    .RequireAuthorization();

detalhe.MapPost("/", async (IDetalheConsultaService detalheConsultaService, DetalheConsultaInput detalheConsulta) =>
{
    var response = await detalheConsultaService.InserirAsync(detalheConsulta);
    return Results.Ok(response);
})
.WithName("Cadastrar detalhe da consulta")
.WithOpenApi();


var medico = app.MapGroup("medico")
    .WithTags("Medico")
    .WithOpenApi()
    .RequireAuthorization();

medico.MapGet("/{id}", async (IMedicoService medicoService, Guid Id) =>
{
    var medicoOutput = await medicoService.ObterPorIdAsync(Id);
    var detalhesOutput = medicoOutput.DetalheConsultas != null ? medicoOutput.DetalheConsultas.Select(x => new DetalheConsultaOutput(x.Valor, x.Descricao)).ToList() : [];
    var response = new MedicoResponse()
    {
        Nome = medicoOutput.Nome,
        Crm = medicoOutput.Crm,
        Especialidade = medicoOutput.Especialidade,
        DetalhesConsulta = detalhesOutput
    };

    return response switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(response)
    };
})
.WithName("Obter Medico")
.WithOpenApi();

medico.MapGet("especialidade/{especialidade}", async (IMedicoService medicoService, string especialidade) =>
{
    var medicoOutput = await medicoService.ObterPorEspecialidadeAsync(especialidade);
    return medicoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(medicoOutput)
    };
})
.WithName("Obter Medico por especialidade")
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
    .WithOpenApi()
    .RequireAuthorization();

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

var agenda = app.MapGroup("agenda")
    .WithTags("Agenda")
    .WithOpenApi()
    .RequireAuthorization();

agenda.MapGet("/{id}", async (IAgendaService agendaService, Guid Id) =>
{
    var agendaOutput = await agendaService.ObterPorIdAsync(Id);
    return agendaOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(agendaOutput)
    };
})
.WithName("Obter Agenda")
.WithOpenApi();

agenda.MapPost("/", async (IAgendaService agendaService, AgendaInput agendaInput) =>
{
    Guid id = await agendaService.InserirAsync(agendaInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Agenda")
.WithOpenApi();

agenda.MapPut("/{id}", async (IAgendaService agendaService, Guid Id, AgendaInput agendaInput) =>
{
    var agendaOutput = await agendaService.AtualizarAsync(Id, agendaInput);
    return agendaOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(agendaOutput)
    };
})
.WithName("Atualizar Agenda")
.WithOpenApi();

agenda.MapDelete("/{id}", async (IAgendaService agendaService, Guid Id) =>
{
    var agendaOutput = await agendaService.RemoverAsync(Id);
    return agendaOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(agendaOutput)
    };
})
.WithName("Remover Agenda")
.WithOpenApi();

var arquivo = app.MapGroup("arquivo")
    .WithTags("Arquivo")
    .WithOpenApi()
    .RequireAuthorization();

arquivo.MapGet("/{id}", async (IArquivoService arquivoService, Guid Id) =>
{
    var arquivoOutput = await arquivoService.ObterPorIdAsync(Id);
    return arquivoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(arquivoOutput)
    };
})
.WithName("Obter Arquivo")
.WithOpenApi();

arquivo.MapPost("/", async (IArquivoService arquivoService, ArquivoInput arquivoInput) =>
{
    Guid id = await arquivoService.InserirAsync(arquivoInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Arquivo")
.WithOpenApi();

arquivo.MapPut("/{id}", async (IArquivoService arquivoService, Guid Id, ArquivoInput arquivoInput) =>
{
    var arquivoOutput = await arquivoService.AtualizarAsync(Id, arquivoInput);
    return arquivoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(arquivoOutput)
    };
})
.WithName("Atualizar Arquivo")
.WithOpenApi();

arquivo.MapDelete("/{id}", async (IArquivoService arquivoService, Guid Id) =>
{
    var arquivoOutput = await arquivoService.RemoverAsync(Id);
    return arquivoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(arquivoOutput)
    };
})
.WithName("Remover Arquivo")
.WithOpenApi();

var prontuario = app.MapGroup("prontuario")
    .WithTags("Prontuario")
    .WithOpenApi()
    .RequireAuthorization();

prontuario.MapGet("/{id}", async (IProntuarioService prontuarioService, Guid Id) =>
{
    var prontuarioOutput = await prontuarioService.ObterPorIdAsync(Id);
    return prontuarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(prontuarioOutput)
    };
})
.WithName("Obter Prontuario")
.WithOpenApi();

prontuario.MapPost("/", async (IProntuarioService prontuarioService, ProntuarioInput prontuarioInput) =>
{
    Guid id = await prontuarioService.InserirAsync(prontuarioInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Prontuario")
.WithOpenApi();

prontuario.MapPut("/{id}", async (IProntuarioService prontuarioService, Guid Id, ProntuarioInput prontuarioInput) =>
{
    var prontuarioOutput = await prontuarioService.AtualizarAsync(Id, prontuarioInput);
    return prontuarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(prontuarioOutput)
    };
})
.WithName("Atualizar Prontuario")
.WithOpenApi();

prontuario.MapDelete("/{id}", async (IProntuarioService prontuarioService, Guid Id) =>
{
    var prontuarioOutput = await prontuarioService.RemoverAsync(Id);
    return prontuarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(prontuarioOutput)
    };
})
.WithName("Remover Prontuario")
.WithOpenApi();

var horario = app.MapGroup("horario")
    .WithTags("Horario")
    .WithOpenApi()
    .RequireAuthorization();

horario.MapGet("/{id}", async (IHorarioService horarioService, Guid Id) =>
{
    var horarioOutput = await horarioService.ObterPorIdAsync(Id);
    return horarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(horarioOutput)
    };
})
.WithName("Obter Horario")
.WithOpenApi();

horario.MapPost("/", async (IHorarioService horarioService, HorarioInput horarioInput) =>
{
    Guid id = await horarioService.InserirAsync(horarioInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Horario")
.WithOpenApi();

horario.MapPut("/{id}", async (IHorarioService horarioService, Guid Id, HorarioInput horarioInput) =>
{
    var horarioOutput = await horarioService.AtualizarAsync(Id, horarioInput);
    return horarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(horarioOutput)
    };
})
.WithName("Atualizar Horario")
.WithOpenApi();

horario.MapDelete("/{id}", async (IHorarioService horarioService, Guid Id) =>
{
    var horarioOutput = await horarioService.RemoverAsync(Id);
    return horarioOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(horarioOutput)
    };
})
.WithName("Remover Horario")
.WithOpenApi();

var consulta = app.MapGroup("consulta")
    .WithTags("Consulta")
    .WithOpenApi()
    .RequireAuthorization();

consulta.MapGet("/{id}", async (IConsultaService consultaService, Guid Id) =>
{
    var consultaOutput = await consultaService.ObterPorIdAsync(Id);
    return consultaOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(consultaOutput)
    };
})
.WithName("Obter Consulta")
.WithOpenApi();

consulta.MapPost("/", async (IConsultaService consultaService, ConsultaInput consultaInput) =>
{
    Guid id = await consultaService.InserirAsync(consultaInput);
    return Results.Created("/", new { id });
})
.WithName("Inserir Consulta")
.WithOpenApi();

consulta.MapPut("/{id}", async (IConsultaService consultaService, Guid Id, ConsultaInput consultaInput) =>
{
    var consultaOutput = await consultaService.AtualizarAsync(Id, consultaInput);
    return consultaOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(consultaOutput)
    };
})
.WithName("Atualizar Consulta")
.WithOpenApi();

consulta.MapDelete("/{id}", async (IConsultaService consultaService, Guid Id, string Justificatuva = null) =>
    {
        var consultaOutput = await consultaService.RemoverAsync(Id, Justificatuva);
        return consultaOutput switch
        {
            null => Results.NotFound("Para deletar a consulta � necessario uma justificativa"),
            _ => Results.Ok(consultaOutput)
        };
})
.WithName("Remover Consulta")
.WithOpenApi();

consulta.MapPatch("/{id}/estado", async (IConsultaService consultaService, Guid Id, EstadoConsulta estado) =>
{
    var result = await consultaService.MudarEstadoAsync(Id, estado);
    return result ? Results.NoContent() : Results.NotFound();
})
.WithName("Mudar Estado Consulta")
.WithOpenApi();

var avaliacao = app.MapGroup("avaliacao")
    .WithTags("Avaliacao")
    .WithOpenApi()
    .RequireAuthorization();

avaliacao.MapGet("/{atendimento}", async (IAvaliacaoService avaliacaoService, Atendimento atendimento) =>
{
    var avaliacaoOutput = await avaliacaoService.RetornaMedicoPorAvaliacaoAsync(atendimento);
    return avaliacaoOutput switch
    {
        null => Results.NotFound(),
        _ => Results.Ok(avaliacaoOutput)
    };
})
.WithName("Obter Medico por avaliacao")
.WithOpenApi();

avaliacao.MapPost("/", async (IAvaliacaoService avaliacaoService, AvaliacaoInput avaliacao) =>
{
    Guid id = await avaliacaoService.InserirAsync(avaliacao);
    return Results.Created("/", new { id });
})
.WithName("Inserir avaliacao")
.WithOpenApi();

var login = app.MapGroup("")
    .WithTags("Login")
    .WithOpenApi();

login.MapPost("cadastrar/", async (ILoginService loginService, Login login) =>
{
    var response = await loginService.CadastrarAsync(login);
    return Results.Created("/", new { response });
})
.WithName("Cadastrar usuario")
.WithOpenApi();

login.MapPost("login/", async (ILoginService loginService, Login login) =>
{
    var response = await loginService.EfetuarLoginAsync(login);
    return Results.Created("/", new { response });
})
.WithName("Login")
.WithOpenApi();

app.Run();

