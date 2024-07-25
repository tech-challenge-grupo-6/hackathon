using ControladorConsulta.Database;
using ControladorConsulta.Models.Medicos;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class MedicoRepository(DatabaseContext databaseContext) : IMedicoRepository
{
    public async Task<Medico?> AtualizarAsync(Medico medico)
    {
        var medicoAtual = await ObterPorIdAsync(medico.Id);
        if (medicoAtual is not null)
        {
            medicoAtual.Nome = medico.Nome;
            medicoAtual.Crm = medico.Crm;
            medicoAtual.Especialidade = medico.Especialidade;
            await databaseContext.SaveChangesAsync();
            return medicoAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Medico medico)
    {
        databaseContext.Medicos.Add(medico);
        await databaseContext.SaveChangesAsync();
        return medico.Id;
    }

    public async Task<Medico?> ObterPorEspecialidadeAsync(string especialidade)
    {
        return await databaseContext.Medicos
            .FirstOrDefaultAsync(medico => medico.Especialidade == especialidade);
    }

    public async Task<Medico?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Medicos
            .Include(medico => medico.DetalheConsultas)
            .FirstOrDefaultAsync(medico => medico.Id == id);
    }

    public async Task<IEnumerable<Medico?>> ObterTodosAsync()
    {
        return await databaseContext.Medicos.ToListAsync();
    }

    public async Task<Medico?> RemoverAsync(Guid id)
    {
        var medicoAtual = await ObterPorIdAsync(id);
        if (medicoAtual is not null)
        {
            databaseContext.Medicos.Remove(medicoAtual);
            await databaseContext.SaveChangesAsync();
            return medicoAtual;
        }
        return null;
    }
}
