using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using System.IO;
using SchoolPracticeDB.Entities;
using SchoolPracticeDB.Models;

namespace SchoolPracticeDB.Controllers
{

    public class CourseController : Controller
    {

        private CourseDbContext _courseDbContext;

        //controller
        public CourseController(CourseDbContext courseDbContext)
        {
            _courseDbContext = courseDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/courses")]
        public IActionResult GetAllCourses()
        {

            var courses = _courseDbContext.Courses
                .Include(c => c.Enrollments).ThenInclude(c => c.Student)
                .Include(c => c.Books)
                .ToList();



            return View("CourseItems", courses);
        }

        [HttpGet("/courses/{id}/edit/edit-request")]
        public IActionResult GetEditRequestCourseById(int id)
        {
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                ActiveCourse = _courseDbContext.Courses.Find(id)
            };

            return View("EditCourse", courseViewModel);
        }

        [HttpPost("/vendors/edit-requests")]
        public IActionResult EditCourse(CourseViewModel courseViewModel)
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

        [HttpGet("/courses/add-request")]
        public IActionResult GetAddCourseRequest()
        {

            CourseViewModel courseViewModel = new CourseViewModel()
            {

                ActiveCourse = new Course()

            };

            return View("AddCourse", courseViewModel);
        }

        [HttpPost("/courses")]
        public IActionResult AddNewCourse(CourseViewModel courseViewModel)
        {

            if(ModelState.IsValid)
            {
                _courseDbContext.Courses.Add(courseViewModel.ActiveCourse);
                _courseDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The course \"{courseViewModel.ActiveCourse.CourseName}\" ({courseViewModel.ActiveCourse.Credits}) was added.";
                return RedirectToAction("GetAllCourses", "Course");
            }
            else
            {

                //remember to return the view AND the proper model for correction
                return View("AddCourse", courseViewModel);
            }

        }

        [HttpGet("/courses/{id}/details")]
        public IActionResult GetCourseById(int id)
        {
            var course = _courseDbContext.Courses
                .Include(c => c.Books)
                .Include(c => c.Enrollments).ThenInclude(e => e.Student)
                .Where(c => c.CourseId == id).FirstOrDefault();

            CourseDetailsViewModel courseViewModel = new CourseDetailsViewModel()
            {
                ActiveCourse = course,
                AverageMark = course.Enrollments.Average(e => e.Grade)
            };

            return View("CourseDetails", courseViewModel);
        }
    }
}
