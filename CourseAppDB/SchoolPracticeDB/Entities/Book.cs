using System.ComponentModel.DataAnnotations;

namespace SchoolPracticeDB.Entities
{
    public class Book
    {

        public int? BookId { get; set; }

        [Required(ErrorMessage = "Please enter a book title.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Please enter an author's name.")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Please enter the year published.")]
        public int? YearPublished { get; set; }


        //FK to course
        public int? CourseId { get; set; }

        //Nav to course
        public Course Course { get; set; }

    }
}
