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
