using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IHorarioRepository
{
    Task<Guid> InserirAsync(Horario horario);
    Task<Horario?> ObterPorIdAsync(Guid id);
    Task<Horario?> RemoverAsync(Guid id);
    Task<Horario?> AtualizarAsync(Horario horario);
    Task<IEnumerable<Horario>> ObterPorIds(IEnumerable<Guid> ids);
}
