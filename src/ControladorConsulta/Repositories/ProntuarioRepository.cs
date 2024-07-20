using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class ProntuarioRepository(DatabaseContext databaseContext) : IProntuarioRepository
{
    public async Task<Prontuario?> AtualizarAsync(Prontuario prontuario)
    {
        var prontuarioAtual = await ObterPorIdAsync(prontuario.Id);
        if (prontuarioAtual is not null)
        {
            prontuarioAtual.Arquivos = prontuario.Arquivos;
            prontuarioAtual.Paciente = prontuario.Paciente;
            prontuarioAtual.PacienteId = prontuario.PacienteId;
            await databaseContext.SaveChangesAsync();
            return prontuarioAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Prontuario prontuario)
    {
        await databaseContext.Prontuarios.AddAsync(prontuario);
        await databaseContext.SaveChangesAsync();
        return prontuario.Id;
    }

    public async Task<Prontuario?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Prontuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Prontuario>> ObterPorIds(IEnumerable<Guid> ids)
    {
        return await databaseContext.Prontuarios.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<Prontuario?> RemoverAsync(Guid id)
    {
        var prontuario = await ObterPorIdAsync(id);
        if (prontuario is not null)
        {
            databaseContext.Prontuarios.Remove(prontuario);
            await databaseContext.SaveChangesAsync();
            return prontuario;
        }
        return null;
    }
}
