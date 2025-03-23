using Fase2.src.models;

namespace Fase2.src.auth
{
    public class UserSession
    {
        private static UserSession? _instance;
        public Usuario? User { get; private set; }

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                _instance ??= new UserSession();
                return _instance;
            }
        }

        public void SetUser(Usuario? user)
        {   
            if (user == null)
            {
                throw new Exception("Se esta intentado Settear un usuario de forma incorrecta, el valor es nulo");
            }
            User = user;
        }

        public void ClearUser()
        {
            User = null;
        }

        public Usuario? GetUser()
        {
            return User;
        }
    }
}