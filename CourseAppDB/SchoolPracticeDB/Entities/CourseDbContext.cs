using Microsoft.EntityFrameworkCore;

namespace SchoolPracticeDB.Entities
{
    public class CourseDbContext : DbContext
    {

        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            :base(options)
        {
        }

        // defining properties for db sets and stuff
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }

        //the link for creating many to many between courses and students
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //creat link between students and courses
            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.StudentId, e.CourseId });

            //set up 1 to Many between courses and enrollment
            modelBuilder.Entity<Enrollment>().HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseId);

            //set up 1 to Many between student and enrollment
            modelBuilder.Entity<Enrollment>().HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId);


            //Seed books
            modelBuilder.Entity<Book>().HasData(
                new Book() { BookId = 1, Title = "Math 101", Author = "Dave Brooks", YearPublished = 2019, CourseId = 1},
                new Book() { BookId = 2, Title = "English Writing for dummies", Author = "William Books", YearPublished = 2009, CourseId = 2 },
                new Book() { BookId = 3, Title = "Holy Stuff", Author = "Bumba York", YearPublished = 2011 , CourseId = 2 },
                new Book() { BookId = 4, Title = "Crazy George", Author = "Dave Brooks", YearPublished = 2020 , CourseId = 1},
                new Book() { BookId = 5, Title = "Huehue 1999", Author = "Dave Brooks", YearPublished = 2011 , CourseId = 3 }

                );

            //Seed courses
            modelBuilder.Entity<Course>().HasData(
                new Course() { CourseId = 1, CourseName = "Math 1", Credits = 2},
                new Course() { CourseId = 2, CourseName = "English Writing", Credits = 1},
                new Course() { CourseId = 3, CourseName = "Network", Credits = 3}
                //new Course() { CourseId = 4, CourseName = "Programming", Credits = 3 }

                );

            //Enrollment
            modelBuilder.Entity<Student>().HasData(
                new Student() { 
                    StudentId = 1,
                    FirstName = "Frank", 
                    LastName = "Brown", 
                    Email = "fbrown@gmail.com",
                    DateOfBirth = new DateTime(2010, 05, 09)},
                new Student() {
                    StudentId = 2,
                    FirstName = "Laura",
                    LastName = "White",
                    Email = "CrazyLaura@gmail.com",
                    DateOfBirth = new DateTime(2011, 11, 04)
                },
                new Student() { 
                    StudentId = 3,
                    FirstName = "Brian",
                    LastName = "Brampton",
                    Email = "thebrampton@gmail.com",
                    DateOfBirth = new DateTime(2012, 01, 02)
                },
                new Student() { 
                    StudentId = 4,
                    FirstName = "Sarah",
                    LastName = "Thompson",
                    Email = "thompsonsarah@gmail.com",
                    DateOfBirth = new DateTime(1988, 01, 19)
                },
                new Student()
                {
                    StudentId = 5,
                    FirstName = "Bobby",
                    LastName = "Grownie",
                    Email = "bobgrownie223@gmail.com",
                    DateOfBirth = new DateTime(2001, 01, 19)
                }

                );

            //casting
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment() { CourseId = 1, StudentId = 1, Grade = 67},
                new Enrollment() { CourseId = 1, StudentId = 2, Grade = 93 },
                new Enrollment() { CourseId = 2, StudentId = 5, Grade = 57 },
                new Enrollment() { CourseId = 2, StudentId = 3, Grade = 67 },
                new Enrollment() { CourseId = 3, StudentId = 4, Grade = 65 },
                new Enrollment() { CourseId = 3, StudentId = 1, Grade = 87 }
                );

        }

    }
}
