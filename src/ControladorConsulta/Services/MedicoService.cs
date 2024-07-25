using ControladorConsulta.Models.Medicos;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class MedicoService(IMedicoRepository medicoRepository) : IMedicoService
{
    public async Task<MedicoOutput?> AtualizarAsync(Guid id, MedicoInput medicoInput)
    {
        var medico = (Medico)medicoInput;
        medico.Id = id;
        var medicoAtualizado = await medicoRepository.AtualizarAsync(medico);
        return medicoAtualizado != null ? (MedicoOutput)medicoAtualizado : null;
    }

    public async Task<Guid> InserirAsync(MedicoInput medicoInput)
    {
        var medico = (Medico)medicoInput;
        Guid id = await medicoRepository.InserirAsync(medico);
        return id;
    }

    public async Task<MedicoOutput?> ObterPorDistanciaAsync()
    {
        var result = await medicoRepository.ObterTodosAsync();
        var medico = result.FirstOrDefault();

        return (MedicoOutput)medico;
    }

    public async Task<MedicoOutput?> ObterPorEspecialidadeAsync(string especialidade)
    {
        var medico = await medicoRepository.ObterPorEspecialidadeAsync(especialidade);
        return medico != null ? (MedicoOutput)medico : null;
    }

    public async Task<MedicoOutput?> ObterPorIdAsync(Guid id)
    {
        var medico = await medicoRepository.ObterPorIdAsync(id);
        return medico != null ? (MedicoOutput)medico : null;
    }

    public async Task<MedicoOutput?> RemoverAsync(Guid id)
    {
        var medico = await medicoRepository.RemoverAsync(id);
        return medico != null ? (MedicoOutput)medico : null;
    }
}
