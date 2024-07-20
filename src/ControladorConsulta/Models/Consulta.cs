namespace ControladorConsulta.Models;

public class Consulta
{
    public Guid Id { get; set; }
    public string LinkTeleconsulta { get; set; } = null!;
    public Guid PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; } = null!;
    public Guid HoraId { get; set; }
    public virtual Horario Horario { get; set; } = null!;
    public Guid ProntuarioId { get; set; }
    public virtual Prontuario Prontuario { get; set; } = null!;

    public EstadoConsulta Estado { get; set; }

    public void GenerateLinkTeleconsulta()
    {
        LinkTeleconsulta = $"https://teleconsulta.com/{Id}";
    }
}

public enum EstadoConsulta
{
    Confirmada,
    Recusada,
    Realizada,
    Cancelada
}

public record ConsultaInput(Guid PacienteId, Guid HoraId, Guid ProntuarioId, EstadoConsulta Estado);

public record ConsultaOutput(Guid Id, string LinkTeleconsulta, Guid PacienteId, Guid HoraId, Guid ProntuarioId, EstadoConsulta Estado)
{
    public static explicit operator ConsultaOutput(Consulta consulta) => new(consulta.Id, consulta.LinkTeleconsulta, consulta.PacienteId, consulta.HoraId, consulta.ProntuarioId, consulta.Estado);
}
