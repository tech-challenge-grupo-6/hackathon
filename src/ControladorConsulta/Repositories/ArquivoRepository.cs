using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class ArquivoRepository(DatabaseContext databaseContext) : IArquivoRepository
{
    public async Task<Arquivo?> AtualizarAsync(Arquivo arquivo)
    {
        var arquivoAtual = await ObterPorIdAsync(arquivo.Id);
        if (arquivoAtual is not null)
        {
            arquivoAtual.Nome = arquivo.Nome;
            arquivoAtual.Url = arquivo.Url;
            arquivoAtual.Acessivel = arquivo.Acessivel;
            arquivoAtual.EpiracaoAcesso = arquivo.EpiracaoAcesso;
            await databaseContext.SaveChangesAsync();
            return arquivoAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Arquivo arquivo)
    {
        await databaseContext.Arquivos.AddAsync(arquivo);
        await databaseContext.SaveChangesAsync();
        return arquivo.Id;
    }

    public async Task<Arquivo?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Arquivos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Arquivo>> ObterPorIds(IEnumerable<Guid> ids)
    {
        return await databaseContext.Arquivos.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<Arquivo?> RemoverAsync(Guid id)
    {
        var arquivo = await ObterPorIdAsync(id);
        if (arquivo is not null)
        {
            databaseContext.Arquivos.Remove(arquivo);
            await databaseContext.SaveChangesAsync();
        }
        return arquivo;
    }
}
