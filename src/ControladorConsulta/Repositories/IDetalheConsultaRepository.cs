using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IDetalheConsultaRepository
{
    Task<Guid> InserirAsync(DetalheConsulta detalheConsulta);
    Task RemoverAsync(Guid id);
    Task<Guid> AtualizarAsync(DetalheConsulta detalheConsulta);
    Task<DetalheConsulta> BuscarPorIdAsync(Guid id);
}
