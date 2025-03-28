using Fase2.src.adts;
using Fase2.src.models;

namespace Fase2.src.services
{
    public class UserService
    {
        private static UserService? _instance;
        private SimpleLinkedList<Usuario> _usersList { get; } = new SimpleLinkedList<Usuario>();
        private UserService() { }
        public static UserService Instance
        {
            get
            {
                _instance ??= new UserService();
                return _instance;
            }
        }

        public void InsertUser(Usuario user)
        {
            if (_usersList.SearchByID(user.ID))
            {
                var id = user.ID;
                throw new Exception($"El usuario con id {id} ya existe.");
            }
            if (_usersList.SearchByEmail(user.Correo))
            {
                var email = user.Correo;
                throw new Exception($"El usuario con email {email} ya existe.");
            }
            _usersList.Insert(user);
        }

        public void UpdateUser(Usuario user)
        {
            if (!_usersList.Update(user))
            {
                var id = user.ID;
                throw new Exception($"El usuario con id {id} no existe.");
            }
        }

        public Usuario FindUserById(int id)
        {
            var user = _usersList.Find(id);
            if (user == null)
            {
                throw new Exception($"El usuario con id {id} no existe.");
            }
            return user;

        }

        public void DeleteUser(int id)
        {
            if (!_usersList.DeleteById(id))
            {
                throw new Exception($"El usuario con id {id} no existe.");
            }
        }

        public bool SearchByEmailAndPass(string email, string password)
        {
            return _usersList.SearchByEmailAndPass(email, password);
        }

        public Usuario? FindUserByEmail(string email)
        {
            return _usersList.FindByEmail(email);
        }

        public bool SearchByID(int id)
        {
            return _usersList.SearchByID(id);
        }
        public void Print()
        {
            _usersList.Print();
        }

        public void GenerateReport()
        {
            if (!_usersList.GenerateReport())
            {
                throw new Exception("No hay usuarios registrados");
            }
        }
    }
}