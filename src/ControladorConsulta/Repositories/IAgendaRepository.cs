using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IAgendaRepository
{
    Task<Guid> InsertAsync(Agenda agenda);
    Task<Agenda?> ObterPorIdAsync(Guid id);
    Task<Agenda?> RemoverAsync(Guid id);
    Task<Agenda?> AtualizarAsync(Agenda agenda);
}
