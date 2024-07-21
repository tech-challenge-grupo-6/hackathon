using ControladorConsulta.Models.Medicos;

namespace ControladorConsulta.Repositories;

public interface IMedicoRepository
{
    Task<Guid> InserirAsync(Medico medico);
    Task<Medico?> ObterPorIdAsync(Guid id);
    Task<Medico?> ObterPorEspecialidadeAsync(string especialidade);
    Task<Medico?> RemoverAsync(Guid id);
    Task<Medico?> AtualizarAsync(Medico medico);
}
