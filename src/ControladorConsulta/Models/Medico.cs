namespace ControladorConsulta.Models;

public class Medico
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Crm { get; set; } = null!;
    public string Especialidade { get; set; } = null!;
    public Guid AgendaId { get; set; }
    public virtual Agenda Agenda { get; set; } = null!;
}

public record MedicoInput(string Nome, string Crm, string Especialidade)
{
    public static explicit operator Medico(MedicoInput medicoInput) => new()
    {
        Nome = medicoInput.Nome,
        Crm = medicoInput.Crm,
        Especialidade = medicoInput.Especialidade
    };
}

public record MedicoOutput(Guid Id, string Nome, string Crm, string Especialidade)
{
    public static explicit operator MedicoOutput(Medico medico) => new(medico.Id, medico.Nome, medico.Crm, medico.Especialidade);
}
