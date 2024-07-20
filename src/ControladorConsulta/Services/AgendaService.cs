using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class AgendaService(
    IAgendaRepository agendaRepository,
    IMedicoRepository medicoRepository,
    IHorarioRepository horarioRepository)
    : IAgendaService
{
    public async Task<AgendaOutput?> AtualizarAsync(Guid id, AgendaInput agendaInput)
    {
        var agendaAtual = await agendaRepository.ObterPorIdAsync(id);
        if (agendaAtual is not null)
        {
            var medico = await medicoRepository.ObterPorIdAsync(agendaInput.MedicoId) ?? throw new ArgumentException("Médico não encontrado");
            var horarios = await horarioRepository.ObterPorIds(agendaInput.HorariosIds);
            agendaAtual.Medico = medico;
            agendaAtual.Horarios = horarios.ToList();
            await agendaRepository.AtualizarAsync(agendaAtual);
            return (AgendaOutput)agendaAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(AgendaInput agendaInput)
    {
        var medico = await medicoRepository.ObterPorIdAsync(agendaInput.MedicoId) ?? throw new ArgumentException("Médico não encontrado");
        var horarios = await horarioRepository.ObterPorIds(agendaInput.HorariosIds);
        var agenda = new Agenda
        {
            Medico = medico,
            Horarios = horarios.ToList()
        };
        return await agendaRepository.InsertAsync(agenda);
    }

    public async Task<AgendaOutput?> ObterPorIdAsync(Guid id)
    {
        var agenda = await agendaRepository.ObterPorIdAsync(id);
        return agenda != null ? (AgendaOutput?)agenda : null;
    }

    public async Task<AgendaOutput?> RemoverAsync(Guid id)
    {
        var agenda = await agendaRepository.RemoverAsync(id);
        return agenda != null ? (AgendaOutput?)agenda : null;
    }
}
