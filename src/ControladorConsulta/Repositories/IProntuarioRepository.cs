using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IProntuarioRepository
{
    Task<Guid> InserirAsync(Prontuario prontuario);
    Task<Prontuario?> ObterPorIdAsync(Guid id);
    Task<Prontuario?> RemoverAsync(Guid id);
    Task<Prontuario?> AtualizarAsync(Prontuario prontuario);
    Task<IEnumerable<Prontuario>> ObterPorIds(IEnumerable<Guid> ids);
}
