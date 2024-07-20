using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IProntuarioService
{
    Task<Guid> InserirAsync(ProntuarioInput prontuarioInput);
    Task<ProntuarioOutput?> ObterPorIdAsync(Guid id);
    Task<ProntuarioOutput?> RemoverAsync(Guid id);
    Task<ProntuarioOutput?> AtualizarAsync(Guid id, ProntuarioInput prontuarioInput);
    Task<IEnumerable<ProntuarioOutput>> ObterPorIds(IEnumerable<Guid> ids);
}
