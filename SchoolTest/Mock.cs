using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SchoolDemo.Data;
using SchoolDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolTest
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
            protected readonly SchoolDbContext _db;

        public Mock() 
        {
            _connection = new SqliteConnection("Filename = :memory:");
            _connection.Open();
            _db = new SchoolDbContext(
                new DbContextOptionsBuilder<SchoolDbContext>()
                .UseSqlite(_connection)
                .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Student> CreateAndSaveTestStudent()
        {
            var student = new Student
            {
                FirstName = "Test",
                LastName = "Student"
            };
            _db.Add(student);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, student.Id);
            return student;
        }

        protected async Task<Course> CreateAndSaveCourse()
        {
            var course = new Course
            {
                CourseCode = "Java",
                TechnologyId = 1,
                Price = 100
            };
            _db.Add(course);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, course.Id);
            return course;

        }
    }
}
