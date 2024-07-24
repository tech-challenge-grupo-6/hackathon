using ControladorConsulta.Models.Medicos;

namespace ControladorConsulta.Models;

public class DetalheConsulta
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public double Valor { get; set; } 
    public Medico Medico { get; set; } = null!;
    public string Descricao { get; set; } = null!;

}


public record DetalheConsultaInput(Guid MedicoId, double Valor, string Descricao);

public record DetalheConsultaOutput(double Valor, string Descricao)
{
    public static explicit operator DetalheConsultaOutput(DetalheConsulta detalheConsulta) => new(detalheConsulta.Valor, detalheConsulta.Descricao);
}
