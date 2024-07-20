using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class ArquivoService(IArquivoRepository arquivoRepository) : IArquivoService
{
    public async Task<ArquivoOutput?> AtualizarAsync(Guid id, ArquivoInput arquivoInput)
    {
        var arquivoAtual = await arquivoRepository.ObterPorIdAsync(id);
        if (arquivoAtual is not null)
        {
            arquivoAtual.Nome = arquivoInput.Nome;
            arquivoAtual.Url = arquivoInput.Url;
            arquivoAtual.Acessivel = arquivoInput.Acessivel;
            arquivoAtual.EpiracaoAcesso = arquivoInput.EpiracaoAcesso;
            await arquivoRepository.AtualizarAsync(arquivoAtual);
            return (ArquivoOutput)arquivoAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(ArquivoInput arquivoInput)
    {
        var arquivo = (Arquivo)arquivoInput;
        return await arquivoRepository.InserirAsync(arquivo);
    }

    public async Task<ArquivoOutput?> ObterPorIdAsync(Guid id)
    {
        var arquivo = await arquivoRepository.ObterPorIdAsync(id);
        return arquivo is null ? null : (ArquivoOutput)arquivo;
    }

    public async Task<IEnumerable<ArquivoOutput>> ObterPorIds(IEnumerable<Guid> ids)
    {
        var arquivos = await arquivoRepository.ObterPorIds(ids);
        return arquivos.Select(arquivo => (ArquivoOutput)arquivo);
    }

    public async Task<ArquivoOutput?> RemoverAsync(Guid id)
    {
        var arquivo = await arquivoRepository.RemoverAsync(id);
        return arquivo is null ? null : (ArquivoOutput)arquivo;
    }
}
