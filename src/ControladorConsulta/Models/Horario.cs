namespace ControladorConsulta.Models;

public class Horario
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public Guid AgendaId { get; set; }
    public virtual Agenda Agenda { get; set; } = null!;
    public virtual Guid ConsultaId { get; set; }
    public virtual Consulta Consulta { get; set; } = null!;
}
