using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class MedicoMapping : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.HasKey(medico => medico.Id);
        builder.Property(medico => medico.Nome).IsRequired();
        builder.Property(medico => medico.Crm).IsRequired();
        builder.Property(medico => medico.Especialidade).IsRequired();
        builder.HasOne(medico => medico.Agenda)
            .WithOne(agenda => agenda.Medico)
            .HasForeignKey<Agenda>(agenda => agenda.MedicoId);
    }
}
