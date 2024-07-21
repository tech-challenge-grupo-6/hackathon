using ControladorConsulta.Models;

namespace ControladorConsulta.Services;

public interface ILoginService
{
    Task<string?> EfetuarLoginAsync(Login login);
    Task<string> CadastrarAsync(Login login);
}
