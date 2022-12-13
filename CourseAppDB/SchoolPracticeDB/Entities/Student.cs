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

        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1850, int.MaxValue, ErrorMessage = "Year must be after 1850.")]
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
