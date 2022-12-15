using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPracticeDB.Entities;
using SchoolPracticeDB.Models;

namespace SchoolPracticeDB.Controllers
{
    public class StudentController : Controller
    {

        private CourseDbContext _courseDbContext;

        //controller
        public StudentController(CourseDbContext courseDbContext)
        {
            _courseDbContext = courseDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/students")]
        public IActionResult GetAllStudents()
        {

            var students = _courseDbContext.Students
                .Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .ToList();

            return View("StudentsList", students);
        }

        [HttpGet("/students/{id}/edit/edit-request")]
        public IActionResult GetEditRequestStudentById(int id)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                ActiveCourse = _courseDbContext.Courses.Find(id)
            };

            return View("EditCourse", courseViewModel);
        }

        [HttpPost("/students/edit-requests")]
        public IActionResult EditStudent(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing movie in the DB:
                _courseDbContext.Courses.Update(courseViewModel.ActiveCourse);
                _courseDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The course \"{courseViewModel.ActiveCourse.CourseName}\" was successfully updated.";

                // and then redirect back to the all vendors view:
                return RedirectToAction("GetAllCourses", "Course");
            }
            else
            {
                return View("EditCourse", courseViewModel);
            }
        }

        [HttpGet("/students/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _courseDbContext.Students
                .Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .Where(s => s.StudentId == id).FirstOrDefault();

            StudentDetailsViewModel studentDetailsViewModel = new StudentDetailsViewModel()
            {
                ActiveStudent = student,
                AverageMark = student.Enrollments.Average(e => e.Grade),
                HighestMark = student.Enrollments.Max(e => e.Grade),
                Courses = _courseDbContext.Courses.OrderBy(c => c.CourseName).ToList()
            };

            return View("StudentDetails", studentDetailsViewModel);
        }

        [HttpPost("/students/{id}/enrollments")]
        public IActionResult AddStudentEnrollmentById(int id, StudentDetailsViewModel studentDetailsViewModel)
        {

            var student = _courseDbContext.Students
                .Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .Where(s => s.StudentId == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                student.Enrollments.Add(studentDetailsViewModel.NewEnrollment);
                _courseDbContext.SaveChanges();
                return RedirectToAction("GetStudentById", "Student", new { id = id });
            }
            else
            {

                studentDetailsViewModel.ActiveStudent = _courseDbContext.Students
                        .Include(s => s.Enrollments).ThenInclude(e => e.Course)
                        .Where(s => s.StudentId == id).FirstOrDefault();

                studentDetailsViewModel.Courses = _courseDbContext.Courses.OrderBy(c => c.CourseName).ToList();

                studentDetailsViewModel.NewEnrollment.Student = studentDetailsViewModel.ActiveStudent;

                return View("StudentDetails", studentDetailsViewModel);
            }

        }

        [HttpGet("/students/add-request")]
        public IActionResult GetAddStudentRequest()
        {

            StudentViewModel studentViewModel = new StudentViewModel()
            {

                ActiveStudent = new Student()

            };

            return View("AddStudent", studentViewModel);
        }

        [HttpPost("/students")]
        public IActionResult AddNewStudent(StudentViewModel studentViewModel)
        {

            //checks if email already exists in db
            if (studentViewModel.ActiveStudent.Email != null)
            {
                var emailAlreadyExists = _courseDbContext.Students.Any(s => s.Email == studentViewModel.ActiveStudent.Email);
                if(emailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "Student with this email already exists");
                }
                
            }

            if (ModelState.IsValid)
            {
                
                _courseDbContext.Students.Add(studentViewModel.ActiveStudent);
                _courseDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The student \"{studentViewModel.ActiveStudent.FullName}\" was added.";
                return RedirectToAction("GetAllCourses", "Course");
            }
            else
            {

                //remember to return the view AND the proper model for correction
                return View("AddStudent", studentViewModel);
            }

        }
    }
}
