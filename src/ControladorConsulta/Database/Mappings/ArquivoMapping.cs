using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class ArquivoMapping : IEntityTypeConfiguration<Arquivo>
{
    public void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        builder.HasKey(arquivo => arquivo.Id);
        builder.Property(arquivo => arquivo.Nome).IsRequired();
        builder.Property(arquivo => arquivo.Url).IsRequired();
        builder.Property(arquivo => arquivo.Acessivel).IsRequired();
        builder.Property(arquivo => arquivo.EpiracaoAcesso).IsRequired();
    }
}
