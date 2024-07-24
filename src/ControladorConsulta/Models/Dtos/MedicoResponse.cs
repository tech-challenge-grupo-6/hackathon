namespace ControladorConsulta.Models.Dtos;

public class MedicoResponse
{
    public string Nome { get; set; } = null!;
    public string Crm { get; set; } = null!;
    public string Especialidade { get; set; } = null!;
    public ICollection<DetalheConsultaOutput> DetalhesConsulta { get; set; } = null!;
}
