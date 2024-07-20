using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IAgendaService
{
    Task<Guid> InserirAsync(AgendaInput agendaInput);
    Task<AgendaOutput?> ObterPorIdAsync(Guid id);
    Task<AgendaOutput?> AtualizarAsync(Guid id, AgendaInput agendaInput);
    Task<AgendaOutput?> RemoverAsync(Guid id);
}
