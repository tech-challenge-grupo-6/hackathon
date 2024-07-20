namespace ControladorConsulta.Models;

public class Horario
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public Guid AgendaId { get; set; }
    public virtual Agenda Agenda { get; set; } = null!;
    public virtual Guid? ConsultaId { get; set; }
    public virtual Consulta? Consulta { get; set; } = null!;
}

public record HorarioInput(DateTime Data, Guid AgendaId, Guid ConsultaId);

public record HorarioOutput(Guid Id, DateTime Data, Guid AgendaId, Guid? ConsultaId)
{
    public static explicit operator HorarioOutput(Horario horario) =>
        new(horario.Id, horario.Data, horario.AgendaId, horario.ConsultaId);
}
