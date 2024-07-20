namespace ControladorConsulta.Models;

public class Prontuario
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; } = null!;
    public virtual ICollection<Arquivo> Arquivos { get; set; } = null!;
}

public record ProntuarioInput(Guid PacienteId, IEnumerable<Guid> ArquivosIds);

public record ProntuarioOutput(Guid Id, Guid PacienteId, IEnumerable<Guid> Arquivos)
{
    public static explicit operator ProntuarioOutput(Prontuario prontuario) =>
        new(prontuario.Id, prontuario.PacienteId, prontuario.Arquivos.Select(arquivo => arquivo.Id));
}
