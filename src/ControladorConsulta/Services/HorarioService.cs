using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class HorarioService(
    IHorarioRepository horarioRepository,
    IAgendaRepository agendaRepository)
    : IHorarioService
{
    public async Task<HorarioOutput?> AtualizarAsync(Guid id, HorarioInput horarioInput)
    {
        var horarioAtual = await horarioRepository.ObterPorIdAsync(id);
        if (horarioAtual is not null)
        {
            var agenda = await agendaRepository.ObterPorIdAsync(horarioInput.AgendaId) ?? throw new ArgumentException("Agenda não encontrada");

            horarioAtual.Agenda = agenda;
            horarioAtual.AgendaId = horarioInput.AgendaId;
            horarioAtual.Data = horarioInput.Data;
            await horarioRepository.AtualizarAsync(horarioAtual);
            return (HorarioOutput)horarioAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(HorarioInput horarioInput)
    {
        var agenda = await agendaRepository.ObterPorIdAsync(horarioInput.AgendaId) ?? throw new ArgumentException("Agenda não encontrada");
        var horario = new Horario
        {
            Data = horarioInput.Data,
            Agenda = agenda,
            AgendaId = horarioInput.AgendaId
        };
        return await horarioRepository.InserirAsync(horario);
    }

    public async Task<HorarioOutput?> ObterPorIdAsync(Guid id)
    {
        var horario = await horarioRepository.ObterPorIdAsync(id);
        return horario is not null ? (HorarioOutput)horario : null;
    }

    public async Task<IEnumerable<HorarioOutput>> ObterPorIds(IEnumerable<Guid> ids)
    {
        var horarios = await horarioRepository.ObterPorIds(ids);
        return horarios.Select(x => (HorarioOutput)x);
    }

    public async Task<HorarioOutput?> RemoverAsync(Guid id)
    {
        var horario = await horarioRepository.RemoverAsync(id);
        return horario is not null ? (HorarioOutput)horario : null;
    }
}
