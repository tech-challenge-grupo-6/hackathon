using ControladorConsulta.Models;
using ControladorConsulta.Repositories;

namespace ControladorConsulta.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IConfiguration _configuration;

    public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
    {
        _loginRepository = loginRepository;
        _configuration = configuration;
    }

    public Task<string> CadastrarAsync(Login login)
    {
        try
        {
            _loginRepository.InserirAsync(login);
            return Task.FromResult("Cadastrado com sucesso!");
        }
        catch (Exception)
        {
            throw;
        }
        
    }

    public async Task<string> EfetuarLoginAsync(Login login)
    {
        return await VerificarLoginAsync(login);
    }

    private async Task<string?> VerificarLoginAsync(Login login)
    {
        switch (login.Tipo)
        {
            case TipoAutenticacao.Paciente:
                {
                    var result = await _loginRepository.ObterLoginAsync();
                    var autenticacao = result
                        .Where(x => x.Email == login.Email && x.Cpf == login.Cpf && x.Senha == login.Senha);

                    return autenticacao.Any() ? _configuration["JWT"] : string.Empty;
                }

            case TipoAutenticacao.Medico:
                {
                    var result = await _loginRepository.ObterLoginAsync();
                    var autenticacao = result
                        .Where(x => x.Crm == login.Crm && x.Senha == login.Senha);

                    return autenticacao.Any() ? _configuration["JWT"] : string.Empty;
                }

            default: throw new NotImplementedException();
        }
    }
}
