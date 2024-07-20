using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IArquivoRepository
{
    Task<Guid> InserirAsync(Arquivo arquivo);
    Task<Arquivo?> ObterPorIdAsync(Guid id);
    Task<Arquivo?> RemoverAsync(Guid id);
    Task<Arquivo?> AtualizarAsync(Arquivo arquivo);
    Task<IEnumerable<Arquivo>> ObterPorIds(IEnumerable<Guid> ids);
}
