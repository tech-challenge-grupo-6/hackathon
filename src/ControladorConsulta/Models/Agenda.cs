namespace ControladorConsulta.Models;

public class Agenda
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public virtual Medico Medico { get; set; } = null!;
    public virtual ICollection<Horario> Horarios { get; set; } = null!;
}
