using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IMedicoService
{
    Task<Guid> InserirAsync(MedicoInput medicoInput);
    Task<MedicoOutput?> ObterPorIdAsync(Guid id);
}
