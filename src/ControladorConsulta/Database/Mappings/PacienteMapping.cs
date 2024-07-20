using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class PacienteMapping : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.HasKey(paciente => paciente.Id);
        builder.Property(paciente => paciente.Nome).IsRequired();
        builder.Property(paciente => paciente.Cpf).IsRequired();
        builder.Property(paciente => paciente.Telefone).IsRequired();
        builder.Property(paciente => paciente.Email).IsRequired();
        builder.HasMany(paciente => paciente.Consultas)
            .WithOne(consulta => consulta.Paciente)
            .HasForeignKey(consulta => consulta.PacienteId);
        builder.HasMany(paciente => paciente.Prontuarios)
            .WithOne(prontuario => prontuario.Paciente)
            .HasForeignKey(prontuario => prontuario.PacienteId);
    }
}
