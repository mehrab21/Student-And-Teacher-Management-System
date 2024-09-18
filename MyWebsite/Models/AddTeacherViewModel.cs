using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class AddTeacherViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? CourseName { get; set; }
        public string? Address { get; set; }
    }
}
