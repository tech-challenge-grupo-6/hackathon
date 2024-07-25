using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class DetalheConsultaRepository(DatabaseContext databaseContext) : IDetalheConsultaRepository
{
    public async Task<Guid> AtualizarAsync(DetalheConsulta detalheConsulta)
    {
        databaseContext.DetalheConsultas.Update(detalheConsulta);
        await databaseContext.SaveChangesAsync();
        return detalheConsulta.Id;
    }

    public async Task<DetalheConsulta?> BuscarPorIdAsync(Guid id)
    {
        var detalhes = await databaseContext.DetalheConsultas.FirstOrDefaultAsync(x => x.Id == id);

        return detalhes;
    }

    public async Task<Guid> InserirAsync(DetalheConsulta detalheConsulta)
    {
        await databaseContext.DetalheConsultas.AddAsync(detalheConsulta);
        await databaseContext.SaveChangesAsync();

        return detalheConsulta.Id;
    }

    public async Task RemoverAsync(Guid id)
    {
        var detalhe = await BuscarPorIdAsync(id);

        if (detalhe != null)
        {
            databaseContext.DetalheConsultas.Remove(detalhe);
            databaseContext.SaveChanges();
        }

    }
}
