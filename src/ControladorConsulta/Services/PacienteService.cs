using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class PacienteService(IPacienteRepository pacienteRepository) : IPacienteService
{
    public async Task<PacienteOutput?> AtualizarAsync(Guid id, PacienteInput pacienteInput)
    {
        var paciente = (Paciente)pacienteInput;
        paciente.Id = id;
        var pacienteAtualizado = await pacienteRepository.AtualizarAsync(paciente);
        return pacienteAtualizado != null ? (PacienteOutput)pacienteAtualizado : null;
    }

    public async Task<Guid> InserirAsync(PacienteInput pacienteInput)
    {
        var paciente = (Paciente)pacienteInput;
        Guid id = await pacienteRepository.InserirAsync(paciente);
        return id;
    }

    public async Task<PacienteOutput?> ObterPorIdAsync(Guid id)
    {
        var paciente = await pacienteRepository.ObterPorIdAsync(id);
        return paciente != null ? (PacienteOutput)paciente : null;
    }

    public async Task<PacienteOutput?> RemoverAsync(Guid id)
    {
        var paciente = await pacienteRepository.RemoverAsync(id);
        return paciente != null ? (PacienteOutput)paciente : null;
    }
}
