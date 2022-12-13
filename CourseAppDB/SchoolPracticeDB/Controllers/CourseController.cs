using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using System.IO;
using SchoolPracticeDB.Entities;

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

    }
}
