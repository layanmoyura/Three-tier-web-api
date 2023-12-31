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
    public class StudentControllerTests
    {
        private Mock<IStudentServices> _studentServicesMock;
        private Fixture _fixture;
        private StudentsController _controller;

        public StudentControllerTests() { 
             
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _studentServicesMock = new Mock<IStudentServices>();
        }

        [TestMethod]
        public async Task GetStudentsAsync_Returnsok()
        {
            var studentsList = _fixture.CreateMany<Student>(3).ToList();
            _studentServicesMock.Setup(service => service.GetStudentsAsync()).Returns(Task.FromResult(studentsList as IEnumerable<Student>));

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.Index();
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));

            var returnedStudents = obj.Value as List<StudentModel>;
            Assert.AreEqual(studentsList.Count, returnedStudents.Count);

            Assert.AreEqual(200, obj.StatusCode);
        }



        [TestMethod]
        public async Task GetStudentDetailsAsync_ReturnsOk()
        {
            var student = _fixture.Create<Student>();
            _studentServicesMock.Setup(service => service.GetStudentDetailsAsync(It.IsAny<int>())).Returns(Task.FromResult(student));

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.Details(1);
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));
            Assert.IsInstanceOfType(obj.Value, typeof(StudentModel));
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task CreateStudentAsync_ReturnsOk()
        {
            var student = _fixture.Create<Student>();
            _studentServicesMock.Setup(service => service.CreateStudentAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.Create(new StudentModel());
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task UpdateStudentAsync_ReturnsOk()
        {
            var student = _fixture.Create<Student>();
            _studentServicesMock.Setup(service => service.UpdateStudentAsync(It.IsAny<Student>(), It.IsAny<int>())).Returns(Task.CompletedTask);

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.EditPost(new StudentModel(), 1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task DeleteStudentAsync_ReturnsOk()
        {
            _studentServicesMock.Setup(service => service.DeleteStudentAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.DeleteConfirm(1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }


        [TestMethod]
        public async Task DeleteStudentAsync_ReturnsBadRequest()
        {
            _studentServicesMock.Setup(service => service.DeleteStudentAsync(It.IsAny<int>())).Throws(new DbUpdateException());

            _controller = new StudentsController(_studentServicesMock.Object);

            var result = await _controller.DeleteConfirm(1);
            var obj = result as BadRequestObjectResult;

            Assert.AreEqual(400, obj.StatusCode);
            
        }


    }
}
