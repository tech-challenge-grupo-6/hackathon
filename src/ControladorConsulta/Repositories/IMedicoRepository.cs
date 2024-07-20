using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IMedicoRepository
{
    Task<Guid> InserirAsync(Medico medico);
    Task<Medico?> ObterPorIdAsync(Guid id);
    Task<Medico?> RemoverAsync(Guid id);
    Task<Medico?> AtualizarAsync(Medico medico);
}
