using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class HorarioMapping : IEntityTypeConfiguration<Horario>
{
    public void Configure(EntityTypeBuilder<Horario> builder)
    {
        builder.HasKey(horario => horario.Id);
        builder.Property(horario => horario.Data).IsRequired();
        builder.HasOne(horario => horario.Agenda)
            .WithMany(agenda => agenda.Horarios)
            .HasForeignKey(horario => horario.AgendaId);
        builder.HasOne(horario => horario.Consulta);
    }
}
