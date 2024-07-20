using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class AgendaRepository(DatabaseContext databaseContext) : IAgendaRepository
{
    public async Task<Agenda?> AtualizarAsync(Agenda agenda)
    {
        var agendaAtual = await ObterPorIdAsync(agenda.Id);
        if (agendaAtual is not null)
        {
            agendaAtual.MedicoId = agenda.MedicoId;
            agendaAtual.Medico = agenda.Medico;
            agendaAtual.Horarios = agenda.Horarios;
            await databaseContext.SaveChangesAsync();
            return agendaAtual;
        }
        return null;
    }

    public async Task<Guid> InsertAsync(Agenda agenda)
    {
        databaseContext.Agendas.Add(agenda);
        await databaseContext.SaveChangesAsync();
        return agenda.Id;
    }

    public async Task<Agenda?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Agendas
            .Include(a => a.Medico)
            .Include(a => a.Horarios)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Agenda?> RemoverAsync(Guid id)
    {
        var agendaAtual = await ObterPorIdAsync(id);
        if (agendaAtual is not null)
        {
            databaseContext.Agendas.Remove(agendaAtual);
            await databaseContext.SaveChangesAsync();
            return agendaAtual;
        }
        return null;
    }
}
