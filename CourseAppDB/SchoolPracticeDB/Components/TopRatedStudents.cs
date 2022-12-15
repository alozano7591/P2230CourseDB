using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPracticeDB.Entities;

namespace SchoolPracticeDB.Components
{
    public class TopRatedStudents : ViewComponent
    {

        CourseDbContext _courseDbContext;

        public TopRatedStudents(CourseDbContext courseDbContext) 
        {
            _courseDbContext = courseDbContext;
        }

        public IViewComponentResult Invoke(int lowestAverage, int numberOfStudentsToDisplay)
        {

            var students = _courseDbContext.Students
                .Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .Where(s => s.Enrollments.Average(e => e.Grade).GetValueOrDefault() >= lowestAverage)
                .OrderBy(s => s.Enrollments.Average(e =>e.Grade).GetValueOrDefault())
                .ToList();
                ;

            TopRatedStudentsViewModel topRatedStudentsViewModel = new TopRatedStudentsViewModel()
            {
                Students = students,
                LowestAverage = lowestAverage,
                NumberOfStudentsToDisplay = numberOfStudentsToDisplay
            };

            return View(topRatedStudentsViewModel);

        }

    }
}
