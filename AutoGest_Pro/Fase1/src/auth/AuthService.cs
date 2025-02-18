namespace Fase1.src.auth
{
    public class AuthService
    {
        public static bool Login(string username, string password)
        {
            return username == "root@gmail.com" && password == "root123";
        }
    }
}