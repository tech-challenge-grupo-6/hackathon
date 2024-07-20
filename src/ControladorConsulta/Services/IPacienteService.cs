using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface IPacienteService
{
    Task<Guid> InserirAsync(PacienteInput pacienteInput);
    Task<PacienteOutput?> ObterPorIdAsync(Guid id);
    Task<PacienteOutput?> RemoverAsync(Guid id);
    Task<PacienteOutput?> AtualizarAsync(Guid id, PacienteInput pacienteInput);
}
