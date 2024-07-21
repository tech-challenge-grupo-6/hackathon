using ControladorConsulta.Models.Medicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(avaliacao => avaliacao.Id);
        builder.Property(avaliacao => avaliacao.Atendimento);
        builder.HasOne(avaliacao => avaliacao.Medico)
            .WithMany(medico => medico.Avaliacoes)
            .HasForeignKey(avaliacao => avaliacao.MedicoId)
            .IsRequired();
        builder.Property(avaliacao => avaliacao.Data);

    }
}
