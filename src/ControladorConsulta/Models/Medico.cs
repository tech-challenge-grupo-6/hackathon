namespace ControladorConsulta.Models;

public class Medico
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Crm { get; set; } = null!;
    public string Especialidade { get; set; } = null!;
    public Guid AgendaId { get; set; }
    public virtual Agenda Agenda { get; set; } = null!;
}
