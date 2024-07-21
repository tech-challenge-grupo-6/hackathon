using ControladorConsulta.Models;

namespace ControladorConsulta.Repositories;

public interface ILoginRepository
{
    Task InserirAsync(Login login);
    Task<List<Login>> ObterLoginAsync();
}
