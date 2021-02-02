using SchoolDemo.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SchoolTest
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task CanEnrollAndDropStudent()
        {
            //arrange
            var student = await CreateAndSaveTestStudent();
            var course = await CreateAndSaveCourse();

            var repository = new CourseRepository(_db);

            //Act
            await repository.AddStudent(course.Id, student.Id);

            //Assertions
            var actualCourse = await repository.GetOne(course.Id);
            Assert.Contains(actualCourse.Enrollments, e => e.StudentId == student.Id);

            //act
            await repository.RemoveStudentFromCourse(course.Id, student.Id);

            //Assert
            Assert.DoesNotContain(actualCourse.Enrollments, e => e.StudentId == student.Id);

        }

    }
}
