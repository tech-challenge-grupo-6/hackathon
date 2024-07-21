namespace ControladorConsulta.Models;

public class Login
{
    public Guid Id { get; set; }
    public string Crm { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public TipoAutenticacao Tipo { get; set; }
}

public enum TipoAutenticacao
{
    Medico = 0,
    Paciente = 1,
}