namespace SchoolPracticeDB.Entities
{
    public class Book
    {

        public int? BookId { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public int? YearPublished { get; set; }


        //FK to course
        public int? CourseId { get; set; }

        //Nav to course
        public Course Course { get; set; }

    }
}
