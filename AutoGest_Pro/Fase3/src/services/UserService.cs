using Fase3.src.adts;
using Fase3.src.models;

namespace Fase3.src.services
{
    public class UserService
    {
        private static UserService? _instance;
        private Blockchain _blockchainUsers { get; } = new Blockchain();
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
            if (_blockchainUsers.SearchByID(user.ID))
            {
                var id = user.ID;
                throw new Exception($"El usuario con id {id} ya existe.");
            }
            // if (_blockchainUsers.SearchByEmail(user.Correo))
            // {
            //     var email = user.Correo;
            //     throw new Exception($"El usuario con email {email} ya existe.");
            // }
            _blockchainUsers.AddBlock(user);
        }

        // public void UpdateUser(Usuario user)
        // {
        //     if (!_usersList.Update(user))
        //     {
        //         var id = user.ID;
        //         throw new Exception($"El usuario con id {id} no existe.");
        //     }
        // }

        public Usuario FindUserById(int id)
        {
            var user = _blockchainUsers.FindUserById(id);
            if (user == null)
            {
                throw new Exception($"El usuario con id {id} no existe.");
            }
            return user;

        }

        // public void DeleteUser(int id)
        // {
        //     if (!_usersList.DeleteById(id))
        //     {
        //         throw new Exception($"El usuario con id {id} no existe.");
        //     }
        // }

        public bool SearchByEmailAndPass(string email, string password)
        {
            return _blockchainUsers.SearchByEmailAndPass(email, password);
        }

        public Usuario? FindUserByEmail(string email)
        {
            return _blockchainUsers.FindByEmail(email);
        }

        public bool SearchByID(int id)
        {
            return _blockchainUsers.SearchByID(id);
        }
        // public void Print()
        // {
        //     _usersList.Print();
        // }

        public void GenerateReport()
        {
            if (!_blockchainUsers.GenerateReport())
            {
                throw new Exception("No hay usuarios registrados para generar reporte.");
            }
        }
    }
}