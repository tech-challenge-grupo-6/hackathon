namespace ControladorConsulta.Models;

public class Prontuario
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; } = null!;
    public Guid ConsultaId { get; set; }
    public virtual Consulta Consulta { get; set; } = null!;
    public virtual ICollection<Arquivo> Arquivos { get; set; } = null!;
}
