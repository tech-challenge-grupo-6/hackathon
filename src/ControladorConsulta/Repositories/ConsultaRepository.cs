using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories;

public class ConsultaRepository(DatabaseContext databaseContext) : IConsultaRepository
{
    public async Task<Consulta?> AtualizarAsync(Consulta consulta)
    {
        var consultaAtual = await ObterPorIdAsync(consulta.Id);
        if (consultaAtual is not null)
        {
            consultaAtual.Prontuario = consulta.Prontuario;
            consultaAtual.ProntuarioId = consulta.ProntuarioId;
            consultaAtual.LinkTeleconsulta = consulta.LinkTeleconsulta;
            consultaAtual.Paciente = consulta.Paciente;
            consultaAtual.PacienteId = consulta.PacienteId;
            consultaAtual.Horario = consulta.Horario;
            consultaAtual.HoraId = consulta.HoraId;
            consultaAtual.Estado = consulta.Estado;
            await databaseContext.SaveChangesAsync();
            return consultaAtual;
        }
        return null;
    }

    public async Task<Guid> InserirAsync(Consulta consulta)
    {
        await databaseContext.Consultas.AddAsync(consulta);
        await databaseContext.SaveChangesAsync();
        return consulta.Id;
    }

    public async Task<bool> MudarEstadoAsync(Guid id, EstadoConsulta estado)
    {
        var consulta = await ObterPorIdAsync(id);
        if (consulta is not null)
        {
            consulta.Estado = estado;
            await databaseContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Consulta?> ObterPorIdAsync(Guid id)
    {
        return await databaseContext.Consultas.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Consulta>> ObterPorIds(IEnumerable<Guid> ids)
    {
        return await databaseContext.Consultas.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<Consulta?> RemoverAsync(Guid id)
    {
        var consulta = await ObterPorIdAsync(id);
        if (consulta is not null)
        {
            databaseContext.Consultas.Remove(consulta);
            await databaseContext.SaveChangesAsync();
            return consulta;
        }
        return null;
    }
}
