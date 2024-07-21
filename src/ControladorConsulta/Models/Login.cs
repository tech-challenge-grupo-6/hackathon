namespace ControladorConsulta.Models;

public class Login
{
    public Guid Id { get; set; }
    public string Crm { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public TipoAutenticacao Tipo { get; set; }
}

public enum TipoAutenticacao
{
    Medico = 0,
    Paciente = 1,
}