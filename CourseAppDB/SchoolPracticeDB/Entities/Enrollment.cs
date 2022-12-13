namespace SchoolPracticeDB.Entities
{
    /// <summary>
    /// Used to link courses and students
    /// </summary>
    public class Enrollment
    {
        //id properties, super necessary
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        //the one thing it will have
        public int? Grade { get; set; }

        //nav properties
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
