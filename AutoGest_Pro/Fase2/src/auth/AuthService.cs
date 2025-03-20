namespace Fase2.src.auth
{
    public enum UserRole
    {
        Admin,
        User,
    }

    public class AuthService
    {
        public static (bool, UserRole?) Login(string username, string password)
        {
            if (username == "root@gmail.com" && password == "root123")
            {
                return (true, UserRole.Admin);
            }
            return (false, null);
        }
    }
}
