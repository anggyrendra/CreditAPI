using CreditAPI.Data;
using CreditAPI.Helpers;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CreditAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return JwtHelper.GenerateToken(user.Id, user.Username, _config);
        }
    }
}
