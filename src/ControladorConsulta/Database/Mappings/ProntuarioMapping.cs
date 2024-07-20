using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class ProntuarioMapping : IEntityTypeConfiguration<Prontuario>
{
    public void Configure(EntityTypeBuilder<Prontuario> builder)
    {
        builder.HasKey(prontuario => prontuario.Id);
        builder.HasOne(prontuario => prontuario.Paciente)
            .WithMany(paciente => paciente.Prontuarios)
            .HasForeignKey(prontuario => prontuario.PacienteId);
        builder.HasOne(prontuario => prontuario.Consulta);
        builder.HasMany(prontuario => prontuario.Arquivos);
    }
}
