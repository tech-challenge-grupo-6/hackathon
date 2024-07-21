namespace ControladorConsulta.Models.Medicos;

public class Avaliacao
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public Medico Medico { get; set; } = null!;
    public Atendimento Atendimento { get; set; } 
    public DateTime Data { get; set; } = DateTime.Now;
}

public record AvaliacaoInput(Guid MedicoId, Atendimento Atendimento, DateTime Data);
