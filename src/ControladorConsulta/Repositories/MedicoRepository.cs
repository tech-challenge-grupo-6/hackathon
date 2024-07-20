using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class MedicoRepository(DatabaseContext databaseContext) : IMedicoRepository
{
    public async Task<Guid> InserirAsync(Medico medico)
    {
        databaseContext.Medicos.Add(medico);
        await databaseContext.SaveChangesAsync();
        return medico.Id;
    }

    public async Task<Medico?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Medicos
            .FirstOrDefaultAsync(medico => medico.Id == id);
    }
}
