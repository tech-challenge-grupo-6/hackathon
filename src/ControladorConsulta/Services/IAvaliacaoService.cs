using ControladorConsulta.Models.Medicos;
using System.Collections;

namespace ControladorConsulta.Services;

public interface IAvaliacaoService
{
    Task<IEnumerable<MedicoOutput>> RetornaMedicoPorAvaliacaoAsync(Atendimento atendimento);
    Task<Guid> InserirAsync(AvaliacaoInput avaliacao);
}
