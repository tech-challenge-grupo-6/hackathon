using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IMedicoService
{
    Task<Guid> InserirAsync(MedicoInput medicoInput);
    Task<MedicoOutput?> ObterPorIdAsync(Guid id);
    Task<MedicoOutput?> RemoverAsync(Guid id);
    Task<MedicoOutput?> AtualizarAsync(Guid id, MedicoInput medicoInput);
}
