using Fase2.src.adts;
using Fase2.src.models;
using Fase2.src.services;

namespace Fase2.src.auth
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
            if (username == "admin@usac.com" && password == "admint123")
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
