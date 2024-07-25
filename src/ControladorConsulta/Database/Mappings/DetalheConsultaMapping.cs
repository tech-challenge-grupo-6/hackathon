using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class DetalheConsultaMapping : IEntityTypeConfiguration<DetalheConsulta>
{
    public void Configure(EntityTypeBuilder<DetalheConsulta> builder)
    {
        builder.HasKey(detalheConsulta => detalheConsulta.Id);
        builder.Property(detalheConsulta => detalheConsulta.Valor);
        builder.Property(detalheConsulta => detalheConsulta.Descricao);
        builder.HasOne(detalheConsulta => detalheConsulta.Medico)
               .WithMany(medico => medico.DetalheConsultas)
               .HasForeignKey(detalheConsulta => detalheConsulta.MedicoId);
    }
}
