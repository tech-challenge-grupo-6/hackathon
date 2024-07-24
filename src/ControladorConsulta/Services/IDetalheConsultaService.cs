using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IDetalheConsultaService
{
    Task<Guid> InserirAsync(DetalheConsultaInput detalheConsulta);
    Task RemoverAsync(Guid id);
}
