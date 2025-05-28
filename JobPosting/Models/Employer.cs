using JobPosting.Enum;

namespace JobPosting.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Employer;
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string ContactNumber { get; set; }
    }
}