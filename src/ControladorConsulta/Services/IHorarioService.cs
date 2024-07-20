using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IHorarioService
{
    Task<Guid> InserirAsync(HorarioInput horarioInput);
    Task<HorarioOutput?> ObterPorIdAsync(Guid id);
    Task<HorarioOutput?> RemoverAsync(Guid id);
    Task<HorarioOutput?> AtualizarAsync(Guid id, HorarioInput horarioInput);
    Task<IEnumerable<HorarioOutput>> ObterPorIds(IEnumerable<Guid> ids);
}
