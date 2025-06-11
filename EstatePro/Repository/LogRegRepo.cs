using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface LogRegRepo
    {
        void Register(User user);
        User Login(string email, string password);
        User GetById(int userId);
        bool EmailExists(string email);
    }
}
