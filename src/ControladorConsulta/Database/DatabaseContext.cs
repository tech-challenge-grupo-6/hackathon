using ControladorConsulta.Models;
using ControladorConsulta.Models.Medicos;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Medico> Medicos { get; set; } = null!;
    public DbSet<Agenda> Agendas { get; set; } = null!;
    public DbSet<Horario> Horarios { get; set; } = null!;
    public DbSet<Consulta> Consultas { get; set; } = null!;
    public DbSet<Paciente> Pacientes { get; set; } = null!;
    public DbSet<Prontuario> Prontuarios { get; set; } = null!;
    public DbSet<Arquivo> Arquivos { get; set; } = null!;
    public DbSet<Avaliacao> Avaliacoes { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
