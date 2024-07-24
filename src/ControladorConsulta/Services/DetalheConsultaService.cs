using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class DetalheConsultaService(IDetalheConsultaRepository detalheConsultaRepository) : IDetalheConsultaService
{
    public async Task<Guid> InserirAsync(DetalheConsultaInput detalheConsulta)
    {
        DetalheConsulta request = new()
        {
            MedicoId = detalheConsulta.MedicoId,
            Valor = detalheConsulta.Valor,
            Descricao = detalheConsulta.Descricao
        };

        var result = await detalheConsultaRepository.InserirAsync(request);
        return result;
    }

    public Task RemoverAsync(Guid id)
    {
        var result = detalheConsultaRepository.RemoverAsync(id);
        return result;
    }
}
