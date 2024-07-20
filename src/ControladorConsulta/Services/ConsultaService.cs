using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class ConsultaService(
    IConsultaRepository consultaRepository,
    IProntuarioRepository prontuarioRepository,
    IPacienteRepository pacienteRepository,
    IHorarioRepository horarioRepository)
    : IConsultaService
{
    public async Task<ConsultaOutput?> AtualizarAsync(Guid id, ConsultaInput consultaInput)
    {
        var consultaAtual = await consultaRepository.ObterPorIdAsync(id);
        if (consultaAtual is not null)
        {
            var prontuario = await prontuarioRepository.ObterPorIdAsync(consultaInput.ProntuarioId) ?? throw new Exception("Prontuário não encontrado");
            var paciente = await pacienteRepository.ObterPorIdAsync(consultaInput.PacienteId) ?? throw new Exception("Paciente não encontrado");
            var horario = await horarioRepository.ObterPorIdAsync(consultaInput.HoraId) ?? throw new Exception("Horário não encontrado");

            consultaAtual.Prontuario = prontuario;
            consultaAtual.ProntuarioId = consultaInput.ProntuarioId;
            consultaAtual.Paciente = paciente;
            consultaAtual.PacienteId = consultaInput.PacienteId;
            consultaAtual.Horario = horario;
            consultaAtual.HoraId = consultaInput.HoraId;
            consultaAtual.Estado = consultaInput.Estado;
            await consultaRepository.AtualizarAsync(consultaAtual);
            return (ConsultaOutput)consultaAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(ConsultaInput consultaInput)
    {
        var prontuario = await prontuarioRepository.ObterPorIdAsync(consultaInput.ProntuarioId) ?? throw new Exception("Prontuário não encontrado");
        var paciente = await pacienteRepository.ObterPorIdAsync(consultaInput.PacienteId) ?? throw new Exception("Paciente não encontrado");
        var horario = await horarioRepository.ObterPorIdAsync(consultaInput.HoraId) ?? throw new Exception("Horário não encontrado");

        var consulta = new Consulta
        {
            Prontuario = prontuario,
            ProntuarioId = consultaInput.ProntuarioId,
            Paciente = paciente,
            PacienteId = consultaInput.PacienteId,
            Horario = horario,
            HoraId = consultaInput.HoraId,
            Estado = consultaInput.Estado
        };
        return await consultaRepository.InserirAsync(consulta);
    }

    public async Task<bool> MudarEstadoAsync(Guid id, EstadoConsulta estado)
    {
        return await consultaRepository.MudarEstadoAsync(id, estado);
    }

    public async Task<ConsultaOutput?> ObterPorIdAsync(Guid id)
    {
        var consulta = await consultaRepository.ObterPorIdAsync(id);
        return consulta is not null ? (ConsultaOutput)consulta : null;
    }

    public async Task<IEnumerable<ConsultaOutput>> ObterPorIds(IEnumerable<Guid> ids)
    {
        var consultas = await consultaRepository.ObterPorIds(ids);
        return consultas.Select(x => (ConsultaOutput)x);
    }

    public async Task<ConsultaOutput?> RemoverAsync(Guid id)
    {
        var consulta = await consultaRepository.RemoverAsync(id);
        return consulta is not null ? (ConsultaOutput)consulta : null;
    }
}
