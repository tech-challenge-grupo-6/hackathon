using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface IPacienteRepository
{
    Task<Guid> InserirAsync(Paciente paciente);
    Task<Paciente?> ObterPorIdAsync(Guid id);
    Task<Paciente?> RemoverAsync(Guid id);
    Task<Paciente?> AtualizarAsync(Paciente paciente);
}
