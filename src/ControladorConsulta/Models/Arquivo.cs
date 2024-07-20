namespace ControladorConsulta.Models;

public class Arquivo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool Acessivel { get; set; }
    public DateTime EpiracaoAcesso { get; set; }
}

public record ArquivoInput(string Nome, string Url, bool Acessivel, DateTime EpiracaoAcesso)
{
    public static explicit operator Arquivo(ArquivoInput arquivoInput) => new()
    {
        Nome = arquivoInput.Nome,
        Url = arquivoInput.Url,
        Acessivel = arquivoInput.Acessivel,
        EpiracaoAcesso = arquivoInput.EpiracaoAcesso
    };
}
public record ArquivoOutput(Guid Id, string Nome, string Url, bool Acessivel, DateTime EpiracaoAcesso)
{
    public static explicit operator ArquivoOutput(Arquivo arquivo) => new(arquivo.Id, arquivo.Nome, arquivo.Url, arquivo.Acessivel, arquivo.EpiracaoAcesso);
}
