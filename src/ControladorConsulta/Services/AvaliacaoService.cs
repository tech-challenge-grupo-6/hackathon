using ControladorConsulta.Models.Medicos;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class AvaliacaoService : IAvaliacaoService
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IMedicoRepository _medicoRepository;
    public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository, IMedicoRepository medicoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _medicoRepository = medicoRepository;
    }

    public async Task<Guid> InserirAsync(AvaliacaoInput avaliacao)
    {

        Medico medico = await _medicoRepository.ObterPorIdAsync(avaliacao.MedicoId) ?? throw new Exception();

        Avaliacao avaliacaoReq = new()
        {
            MedicoId = avaliacao.MedicoId,
            Atendimento = avaliacao.Atendimento,
            Medico = medico
        };

        var result = await _avaliacaoRepository.InserirAsync(avaliacaoReq);
        return result;
    }

    public async Task<IEnumerable<MedicoOutput>> RetornaMedicoPorAvaliacaoAsync(Atendimento atendimento)
    {
        var todos = await _avaliacaoRepository.ObterTodosAsync();

        var filtraPorAtendimento = todos
            .Where(a => a.Atendimento == atendimento)
            .GroupBy(a => a.Medico)
            .Select(g => new
            {
                Medico = g.Key,
                Contagem = g.Count()
            })
            .OrderByDescending(x => x.Contagem) 
            .Take(5)
            .Select(x=> (MedicoOutput)x.Medico)
            .ToList();

        return filtraPorAtendimento;
    }
}
