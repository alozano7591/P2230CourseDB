using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Design;
using SchoolPracticeDB.Entities;
using System.Text.RegularExpressions;
using SchoolPracticeDB.Models;

namespace Courses.Tests
{
    public class CourseTest
    {

        [Fact]
        public void TestAverageCourseGrade()
        {
            //Arrange
            Course c1 = new Course()
            {
                CourseId= 1,
                CourseName = "Math 1",
                Credits = 2,
                Enrollments = new List<Enrollment>()   //NEEDS LIST TO ADD MARKS TO!
            };

            //DOES NOT NEED STUDENT EVEN THOUGH STUDENT IS APART OF ENROLLMENT
            //Student s1= new Student()
            //{
            //    StudentId= 1,
            //    FirstName = "Bob",
            //    LastName = "Last",
            //}

            c1.Enrollments.Add(new Enrollment() { Grade = 78});
            c1.Enrollments.Add(new Enrollment() { Grade = 88 });
            c1.Enrollments.Add(new Enrollment() { Grade = 68 });
            c1.Enrollments.Add(new Enrollment() { Grade = 58 });

            double averageGrade = c1.Enrollments.Average(e => e.Grade).GetValueOrDefault();

            //Assert
            Assert.Equal(73, averageGrade);
        }

        /// <summary>
        /// Tests for highest grade
        /// </summary>
        [Fact]
        public void TestMaxStudentGrade()
        {
            //Arrange
            Student s1 = new Student()
            {
                FirstName = "Bob",
                LastName = "Brown",
                Enrollments = new List<Enrollment>()   //NEEDS LIST TO ADD MARKS TO!
            };

            s1.Enrollments.Add(new Enrollment() { Grade = 78 });
            s1.Enrollments.Add(new Enrollment() { Grade = 88 });
            s1.Enrollments.Add(new Enrollment() { Grade = 68 });
            s1.Enrollments.Add(new Enrollment() { Grade = 58 });

            double averageGrade = s1.Enrollments.Max(e => e.Grade).GetValueOrDefault();

            //Assert
            Assert.Equal(88, averageGrade);
        }

        //[Fact]
        //public void TestStudentAgeCheck()
        //{
        //    //Arrange
        //    Student s1 = new Student()
        //    {
        //        FirstName = "Bob",
        //        LastName = "Brown",
        //        DateOfBirth = new DateTime(2012, 01, 01),
        //        Enrollments = new List<Enrollment>()   //NEEDS LIST TO ADD MARKS TO!
        //    };

            

        //    //Assert
        //    Assert.True(false, MinimumAgeAttribute(s1.DateOfBirth));
        //}

    }
}
