using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class PacienteRepository(DatabaseContext databaseContext) : IPacienteRepository
{
    public async Task<Paciente?> AtualizarAsync(Paciente paciente)
    {
        var pacienteAtual = await ObterPorIdAsync(paciente.Id);
        if (pacienteAtual is not null)
        {
            pacienteAtual.Nome = paciente.Nome;
            pacienteAtual.Cpf = paciente.Cpf;
            pacienteAtual.Email = paciente.Email;
            pacienteAtual.Telefone = paciente.Telefone;
            await databaseContext.SaveChangesAsync();
            return pacienteAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Paciente paciente)
    {
        databaseContext.Pacientes.Add(paciente);
        await databaseContext.SaveChangesAsync();
        return paciente.Id;
    }

    public async Task<Paciente?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Pacientes
            .FirstOrDefaultAsync(paciente => paciente.Id == id);
    }

    public async Task<Paciente?> RemoverAsync(Guid id)
    {
        var pacienteAtual = await ObterPorIdAsync(id);
        if (pacienteAtual is not null)
        {
            databaseContext.Pacientes.Remove(pacienteAtual);
            await databaseContext.SaveChangesAsync();
            return pacienteAtual;
        }
        return null;
    }
}
