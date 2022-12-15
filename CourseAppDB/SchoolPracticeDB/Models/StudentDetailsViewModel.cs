using SchoolPracticeDB.Entities;

namespace SchoolPracticeDB.Models
{
    public class StudentDetailsViewModel
    {

        public Student ActiveStudent { get; set; }

        public double? HighestMark { get; set; }

        public double? AverageMark { get; set; }

        public Enrollment? NewEnrollment { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
