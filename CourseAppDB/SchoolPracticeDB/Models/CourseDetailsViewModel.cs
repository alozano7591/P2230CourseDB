using SchoolPracticeDB.Entities;

namespace SchoolPracticeDB.Models
{
    public class CourseDetailsViewModel
    {

        public Course? ActiveCourse { get; set; }

        public Book? NewBook { get; set; }

        public double? AverageMark { get; set; }

    }
}
