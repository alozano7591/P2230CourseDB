namespace SchoolPracticeDB.Entities
{
    public class Course
    {

        public int CourseId { get; set; }

        public string? CourseName { get; set; }

        public int? Credits { get; set; }

        //nav to students
        public ICollection<Enrollment>? Enrollments { get; set; }

        //nav to all of its books (zero to many)
        public ICollection<Book>? Books { get; set; }

    }
}
