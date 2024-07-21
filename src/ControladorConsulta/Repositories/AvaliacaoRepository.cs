using ControladorConsulta.Database;
using ControladorConsulta.Models.Medicos;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly DatabaseContext _dbContext;
    public AvaliacaoRepository(DatabaseContext databaseContext)
    {
        _dbContext = databaseContext;
    }

    public async Task<Guid> InserirAsync(Avaliacao avaliacao)
    {
        Avaliacao avaliacaoReq = new() { Atendimento = avaliacao.Atendimento, MedicoId = avaliacao.MedicoId };

        await _dbContext.Avaliacoes.AddAsync(avaliacaoReq);
        await _dbContext.SaveChangesAsync();
        return avaliacaoReq.Id;
    }

    public async Task<List<Avaliacao>> ObterTodosAsync()
    {
        var result = await _dbContext.Avaliacoes
            .Include(a => a.Medico)
            .ToListAsync();
       
        return result;
    }
}
