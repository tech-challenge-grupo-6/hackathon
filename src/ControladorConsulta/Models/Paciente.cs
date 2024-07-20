namespace ControladorConsulta.Models;

public class Paciente
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public virtual ICollection<Prontuario> Prontuarios { get; set; } = null!;
    public virtual ICollection<Consulta> Consultas { get; set; } = null!;
}

public record PacienteInput(string Nome, string Cpf, string Email, string Telefone)
{
    public static explicit operator Paciente(PacienteInput pacienteInput) =>
        new()
        {
            Nome = pacienteInput.Nome,
            Cpf = pacienteInput.Cpf,
            Email = pacienteInput.Email,
            Telefone = pacienteInput.Telefone
        };
}

public record PacienteOutput(Guid Id, string Nome, string Cpf, string Email, string Telefone)
{
    public static explicit operator PacienteOutput(Paciente paciente) =>
        new(paciente.Id, paciente.Nome, paciente.Cpf, paciente.Email, paciente.Telefone);
}
