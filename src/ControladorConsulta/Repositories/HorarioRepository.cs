using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class HorarioRepository(DatabaseContext databaseContext) : IHorarioRepository
{
    public async Task<Horario?> AtualizarAsync(Horario horario)
    {
        var horarioAtual = await ObterPorIdAsync(horario.Id);
        if (horarioAtual is not null)
        {
            horarioAtual.Agenda = horario.Agenda;
            horarioAtual.AgendaId = horario.AgendaId;
            horarioAtual.Data = horario.Data;
            horarioAtual.Consulta = horario.Consulta;
            horarioAtual.ConsultaId = horario.ConsultaId;
            await databaseContext.SaveChangesAsync();
            return horarioAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Horario horario)
    {
        await databaseContext.Horarios.AddAsync(horario);
        await databaseContext.SaveChangesAsync();
        return horario.Id;
    }

    public async Task<Horario?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Horarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Horario>> ObterPorIds(IEnumerable<Guid> ids)
    {
        return await databaseContext.Horarios.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<Horario?> RemoverAsync(Guid id)
    {
        var horario = await ObterPorIdAsync(id);
        if (horario is not null)
        {
            databaseContext.Horarios.Remove(horario);
            await databaseContext.SaveChangesAsync();
            return horario;
        }
        return null;
    }
}
