using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IArquivoService
{
    Task<Guid> InserirAsync(ArquivoInput arquivoInput);
    Task<ArquivoOutput?> ObterPorIdAsync(Guid id);
    Task<ArquivoOutput?> RemoverAsync(Guid id);
    Task<ArquivoOutput?> AtualizarAsync(Guid id, ArquivoInput arquivoInput);
    Task<IEnumerable<ArquivoOutput>> ObterPorIds(IEnumerable<Guid> ids);
}
