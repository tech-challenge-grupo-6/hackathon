using ControladorConsulta.Database;
using ControladorConsulta.Models;
using Microsoft.EntityFrameworkCore;

namespace ControladorConsulta.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LoginRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task InserirAsync(Login login)
        {
            await _databaseContext.Logins.AddAsync(login);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Login>> ObterLoginAsync()
        {
            var result = await _databaseContext.Logins.ToListAsync();
            return result;
        }
    }
}
