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

    public void GenerateLinkTeleconsulta()
    {
        LinkTeleconsulta = $"https://teleconsulta.com/{Id}";
    }
}
