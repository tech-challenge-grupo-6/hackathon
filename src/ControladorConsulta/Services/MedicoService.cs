using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class MedicoService(IMedicoRepository medicoRepository) : IMedicoService
{
    public async Task<Guid> InserirAsync(MedicoInput medicoInput)
    {
        var medico = (Medico)medicoInput;
        Guid id = await medicoRepository.InserirAsync(medico);
        return id;
    }

    public async Task<MedicoOutput?> ObterPorIdAsync(Guid id)
    {
        var medico = await medicoRepository.ObterPorIdAsync(id);
        return medico != null ? (MedicoOutput)medico : null;
    }
}
