using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IConsultaService
{
    Task<Guid> InserirAsync(ConsultaInput consultaInput);
    Task<ConsultaOutput?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<ConsultaOutput>> ObterPorIds(IEnumerable<Guid> ids);
    Task<ConsultaOutput?> RemoverAsync(Guid id, string justificatica);
    Task<ConsultaOutput?> AtualizarAsync(Guid id, ConsultaInput consultaInput);
    Task<bool> MudarEstadoAsync(Guid id, EstadoConsulta estado);
}
