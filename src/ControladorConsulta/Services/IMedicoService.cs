using ControladorConsulta.Models.Medicos;

namespace ControladorConsulta.Services;

public interface IMedicoService
{
    Task<Guid> InserirAsync(MedicoInput medicoInput);
    Task<MedicoOutput?> ObterPorIdAsync(Guid id);
    Task<MedicoOutput?> ObterPorEspecialidadeAsync(string especialidade);
    Task<MedicoOutput?> RemoverAsync(Guid id);
    Task<MedicoOutput?> AtualizarAsync(Guid id, MedicoInput medicoInput);
    Task<MedicoOutput?> ObterPorDistanciaAsync();
}
