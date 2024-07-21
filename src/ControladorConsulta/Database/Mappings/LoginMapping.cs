using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControladorConsulta.Database.Mappings;

public class LoginMapping : IEntityTypeConfiguration<Login>
{
    public void Configure(EntityTypeBuilder<Login> builder)
    {
        builder.HasKey(login => login.Id);
        builder.Property(login => login.Cpf);
        builder.Property(login => login.Tipo);
        builder.Property(login => login.Senha);
        builder.Property(login => login.Email);
        builder.Property(login => login.Crm);
    }
}
