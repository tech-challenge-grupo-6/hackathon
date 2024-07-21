using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControladorConsulta.Models;
using ControladorConsulta.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace ControladorConsulta.Services;

public class LoginService(ILoginRepository loginRepository) : ILoginService
{
    public Task<string> CadastrarAsync(Login login)
    {
        try
        {
            loginRepository.InserirAsync(login);
            return Task.FromResult("Cadastrado com sucesso!");
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<string?> EfetuarLoginAsync(Login login)
    {
        return await VerificarLoginAsync(login);
    }

    private static SigningCredentials GerarCredenciais()
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MDEyMzQ1Njc4OUFCQ0RFRjAxMjM0NTY3ODlBQkNERUY="));
        return new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
    }

    private async Task<string?> VerificarLoginAsync(Login login)
    {
        switch (login.Tipo)
        {
            case TipoAutenticacao.Paciente:
                {
                    var result = await loginRepository.ObterLoginAsync();
                    var autenticacao = result
                        .Where(x => x.Email == login.Email && x.Cpf == login.Cpf && x.Senha == login.Senha);

                    if (autenticacao.Any())
                    {
                        var token = new JwtSecurityToken(
                            issuer: "dotnet-user-jwts",
                            audience: "http://localhost:59096, https://localhost:44393, http://localhost:5005",
                            expires: DateTime.Now.AddMinutes(30),
                            claims:
                            [
                                new (ClaimTypes.Email, login.Email),
                                new(ClaimTypes.NameIdentifier, login.Cpf),
                                new (ClaimTypes.Role, "Paciente")
                            ],
                            signingCredentials: GerarCredenciais());
                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                    return string.Empty;
                }

            case TipoAutenticacao.Medico:
                {
                    var result = await loginRepository.ObterLoginAsync();
                    var autenticacao = result
                        .Where(x => x.Crm == login.Crm && x.Senha == login.Senha);

                    if (autenticacao.Any())
                    {
                        var token = new JwtSecurityToken(
                            issuer: "dotnet-user-jwts",
                            audience: "http://localhost:59096, https://localhost:44393, http://localhost:5005",
                            expires: DateTime.Now.AddMinutes(30),
                            claims:
                            [
                                new (ClaimTypes.Email, login.Email),
                                new(ClaimTypes.NameIdentifier, login.Crm),
                                new (ClaimTypes.Role, "Medico")
                            ],
                            signingCredentials: GerarCredenciais());
                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                    return string.Empty;
                }

            default: throw new NotImplementedException();
        }
    }
}
