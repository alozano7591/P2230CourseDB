using SchoolPracticeDB.Entities;

namespace SchoolPracticeDB.Components
{
    public class TopRatedStudentsViewModel
    {

        public List<Student>? Students { get; set; }

        public int LowestAverage { get; set; }

        public int NumberOfStudentsToDisplay { get; set; }
    }
}
