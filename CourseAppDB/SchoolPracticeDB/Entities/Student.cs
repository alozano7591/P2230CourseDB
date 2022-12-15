using SchoolPracticeDB.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolPracticeDB.Entities
{
    public class Student
    {

        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string? LastName { get; set; }

        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //[Range(1850, int.MaxValue, ErrorMessage = "Year must be after 1850.")]
        [Required(ErrorMessage = "Please enter a year.")]
        [MinimumAge(12, ErrorMessage = "Must be 12 or older")]
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
