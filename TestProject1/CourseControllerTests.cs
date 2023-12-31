using AutoFixture;
using BusinessLayer.Interfaces;
using ContosoUniversity.Controllers;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PresentationLayer.Models;

namespace TestProject1
{
    [TestClass]
    public class CourseControllerTests
    {
        private Mock<ICourseServices> _courseServicesMock;
        private Fixture _fixture;
        private CoursesController _courseController;

        public CourseControllerTests()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _courseServicesMock = new Mock<ICourseServices>();
        }

        [TestMethod]
        public async Task GetCoursesAsync_ReturnsOK()
        {
            var courseList = _fixture.CreateMany<Course>(3).ToList();
            _courseServicesMock.Setup(service=>service.GetCoursesAsync()).Returns(Task.FromResult(courseList as IEnumerable<Course>));

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.Index();
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));

            var returnedCourses = obj.Value as List<CourseModel>;
            Assert.AreEqual(courseList.Count,returnedCourses.Count());

            Assert.AreEqual(200, obj.StatusCode);

        }

        [TestMethod]
        public async Task GetCourseByIdAsync()
        {
            var course = _fixture.Create<Course>();
            _courseServicesMock.Setup(service=>service.GetCourseByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(course));

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.Details(1);
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));
            Assert.IsInstanceOfType(obj.Value, typeof(CourseModel));
            Assert.AreEqual(200, obj.StatusCode);
        }


        [TestMethod]
        public async Task AddCourseAsync_ReturnsOk()
        {
            _courseServicesMock.Setup(service => service.AddCourseAsync(It.IsAny<Course>())).Returns(Task.CompletedTask);

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.Create(new CourseModel());
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task UpdateCourseAsync_ReturnsOk()
        {
            _courseServicesMock.Setup(service => service.UpdateCourseAsync(It.IsAny<Course>(), It.IsAny<int>())).Returns(Task.CompletedTask);

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.EditPost(new CourseModel(), 1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task DeleteCourseAsync_ReturnsOk()
        {
            _courseServicesMock.Setup(service => service.DeleteCourseAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.DeleteConfirm(1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task DeleteStudentAsync_ReturnsBadRequest()
        {
            _courseServicesMock.Setup(service => service.DeleteCourseAsync(It.IsAny<int>())).Throws(new DbUpdateException());

            _courseController = new CoursesController(_courseServicesMock.Object);

            var result = await _courseController.DeleteConfirm(1);
            var obj = result as BadRequestObjectResult;

            Assert.AreEqual(400, obj.StatusCode);

        }




    }
}
