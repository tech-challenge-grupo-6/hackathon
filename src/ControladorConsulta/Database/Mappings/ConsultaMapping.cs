using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.HasKey(consulta => consulta.Id);
        builder.Property(consulta => consulta.LinkTeleconsulta).IsRequired();
        builder.HasOne(consulta => consulta.Paciente)
            .WithMany(paciente => paciente.Consultas)
            .HasForeignKey(consulta => consulta.PacienteId);
        builder.HasOne(consulta => consulta.Paciente)
            .WithMany(paciente => paciente.Consultas)
            .HasForeignKey(consulta => consulta.PacienteId);
        builder.HasOne(consulta => consulta.Horario);
        builder.Property(consulta => consulta.Estado);
    }
}
