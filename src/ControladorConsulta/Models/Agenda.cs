namespace ControladorConsulta.Models;

public class Agenda
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public virtual Medico Medico { get; set; } = null!;
    public virtual ICollection<Horario> Horarios { get; set; } = null!;
}

public record AgendaInput(Guid MedicoId, Guid[] HorariosIds);

public record AgendaOutput(Guid Id, Guid MedicoId, IEnumerable<Guid> HorariosIds)
{
    public static explicit operator AgendaOutput(Agenda agenda) => new(agenda.Id, agenda.MedicoId, agenda.Horarios.Select(h => h.Id));
}