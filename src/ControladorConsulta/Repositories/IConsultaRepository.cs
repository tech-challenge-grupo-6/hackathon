using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IConsultaRepository
{
    Task<Guid> InserirAsync(Consulta consulta);
    Task<Consulta?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Consulta>> ObterPorIds(IEnumerable<Guid> ids);
    Task<Consulta?> RemoverAsync(Guid id);
    Task<Consulta?> AtualizarAsync(Consulta consulta);
    Task<bool> MudarEstadoAsync(Guid id, EstadoConsulta estado);
}
