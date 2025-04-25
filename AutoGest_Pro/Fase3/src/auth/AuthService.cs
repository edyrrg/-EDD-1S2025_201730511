using Fase3.src.adts;
using Fase3.src.models;
using Fase3.src.services;

namespace Fase3.src.auth
{
    public enum UserRole
    {
        Admin,
        User,
    }

    public class AuthService
    {
        public static (bool, UserRole?) Login(string username, string password, UserService users)
        {
            if (username == "admin@usac.com" && password == "admin123")
            {
                return (true, UserRole.Admin);
            }
            if (users.SearchByEmailAndPass(username, password))
            {
                return (true, UserRole.User);
            }
            return (false, null);
        }
    }
}
