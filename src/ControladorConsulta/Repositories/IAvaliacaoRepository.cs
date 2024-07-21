using ControladorConsulta.Models.Medicos;

namespace ControladorConsulta.Repositories;

public interface IAvaliacaoRepository
{
    Task<Guid> InserirAsync(Avaliacao avaliacao);
    Task<List<Avaliacao>> ObterTodosAsync();

}
