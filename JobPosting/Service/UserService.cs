
using JobPosting.Models;
using JobPosting.DataBase;
using JobPosting.Enum;

namespace JobPosting.Services
{
    public class UserService
    {

        public User Login(LoginModel model)
        {
            var user = Database.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                return null; 
            }
            return user; 
        }

        public User Register(RegisterModel model)
        {
            var user = new User
            {
                Id = Database.Users.Count + 1,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = UserRole.JobSeeker 
            };
            Database.Users.Add(user);
            return user;
        }

        public User CreateEmployer(string name, string email, string password)
        {
            var user = new User
            {
                Id = Database.Users.Count + 1,
                Name = name,
                Email = email,
                Password = password,
                Role = UserRole.Employer
            };
            Database.Users.Add(user);
            return user;
        }
    }
}