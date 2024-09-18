using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string? Address { get; set; }
        public string? Hobby { get; set; }

    }
}
