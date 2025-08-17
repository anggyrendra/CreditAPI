using CreditAPI.Models;

namespace CreditAPI.Services
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
    }
}
