using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class ProntuarioService(
    IProntuarioRepository prontuarioRepository,
    IArquivoRepository arquivoRepository,
    IPacienteRepository pacienteRepository)
    : IProntuarioService
{
    public async Task<ProntuarioOutput?> AtualizarAsync(Guid id, ProntuarioInput prontuarioInput)
    {
        var prontuarioAtual = await prontuarioRepository.ObterPorIdAsync(id);
        if (prontuarioAtual is not null)
        {
            var arquivos = await arquivoRepository.ObterPorIds(prontuarioInput.ArquivosIds);
            prontuarioAtual.Arquivos = arquivos.ToList();

            var paciente = await pacienteRepository.ObterPorIdAsync(prontuarioInput.PacienteId) ?? throw new Exception("Paciente não encontrado");
            prontuarioAtual.Paciente = paciente;
            prontuarioAtual.PacienteId = prontuarioInput.PacienteId;
            await prontuarioRepository.AtualizarAsync(prontuarioAtual);
            return (ProntuarioOutput)prontuarioAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(ProntuarioInput prontuarioInput)
    {
        var arquivos = await arquivoRepository.ObterPorIds(prontuarioInput.ArquivosIds);
        var paciente = await pacienteRepository.ObterPorIdAsync(prontuarioInput.PacienteId) ?? throw new Exception("Paciente não encontrado");
        var prontuario = new Prontuario
        {
            Arquivos = arquivos.ToList(),
            Paciente = paciente,
            PacienteId = prontuarioInput.PacienteId
        };
        return await prontuarioRepository.InserirAsync(prontuario);
    }

    public async Task<ProntuarioOutput?> ObterPorIdAsync(Guid id)
    {
        var prontuario = await prontuarioRepository.ObterPorIdAsync(id);
        return prontuario is not null ? (ProntuarioOutput)prontuario : null;
    }

    public async Task<IEnumerable<ProntuarioOutput>> ObterPorIds(IEnumerable<Guid> ids)
    {
        var prontuarios = await prontuarioRepository.ObterPorIds(ids);
        return prontuarios.Select(prontuario => (ProntuarioOutput)prontuario);
    }

    public async Task<ProntuarioOutput?> RemoverAsync(Guid id)
    {
        var prontuario = await prontuarioRepository.RemoverAsync(id);
        return prontuario is not null ? (ProntuarioOutput)prontuario : null;
    }
}
