using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class LogRegService : LogRegRepo
    {
        ApplicationDbContext db;

        public LogRegService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Register(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var data = db.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            return data;
        }

        public User GetById(int userId)
        {
            var data = db.Users.FirstOrDefault(u => u.UserId == userId);
            return data;
        }

        public bool EmailExists(string email)
        {
            var data= db.Users.Any(u => u.Email == email);
            return data;
        }
    }
}
