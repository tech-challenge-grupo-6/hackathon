using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class AgendaMapping : IEntityTypeConfiguration<Agenda>
{
    public void Configure(EntityTypeBuilder<Agenda> builder)
    {
        builder.HasKey(agenda => agenda.Id);
        builder.HasOne(agenda => agenda.Medico);
        builder.HasMany(agenda => agenda.Horarios)
            .WithOne(horario => horario.Agenda)
            .HasForeignKey(horario => horario.AgendaId);
    }
}
