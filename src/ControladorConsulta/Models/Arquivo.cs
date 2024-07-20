namespace ControladorConsulta.Models;

public class Arquivo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool Acessivel { get; set; }
    public DateTime EpiracaoAcesso { get; set; }
}
