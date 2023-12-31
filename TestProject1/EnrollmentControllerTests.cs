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
    public class EnrollmentControllerTests
    {
        private Mock<IEnrollmentService> _enrollmentServicesMock;
        private Fixture _fixture;
        private EnrollmentsController _enrollmentController;

        public EnrollmentControllerTests()
        {

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _enrollmentServicesMock = new Mock<IEnrollmentService>();
        }

        [TestMethod]
        public async Task GetAllEnrollments_ReturnsOk()
        {
            var enrollmentList = _fixture.CreateMany<Enrollment>(3).ToList();
            _enrollmentServicesMock.Setup(service => service.GetAllEnrollments()).Returns(Task.FromResult(enrollmentList));

            _enrollmentController = new EnrollmentsController(_enrollmentServicesMock.Object);

            var result = await _enrollmentController.Index();
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));

            var returnedEnrollments = obj.Value as List<EnrollmentModel>;
            Assert.AreEqual(enrollmentList.Count, returnedEnrollments.Count());

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetEnrollmentByIdAsync_ReturnsOk()
        {
            var enrollment = _fixture.Create<Enrollment>();
            _enrollmentServicesMock.Setup(service => service.GetEnrollmentById(It.IsAny<int>())).Returns(Task.FromResult(enrollment));

            _enrollmentController = new EnrollmentsController(_enrollmentServicesMock.Object);

            var result = await _enrollmentController.Details(1);
            var obj = result as ObjectResult;

            Assert.IsInstanceOfType(obj, typeof(OkObjectResult));
            Assert.IsInstanceOfType(obj.Value, typeof(EnrollmentModel));
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task AddEnrollmentAsync_ReturnsOk()
        {
            _enrollmentServicesMock.Setup(service => service.AddEnrollment(It.IsAny<Enrollment>())).Returns(Task.CompletedTask);

            _enrollmentController = new EnrollmentsController(_enrollmentServicesMock.Object);

            var result = await _enrollmentController.Create(new EnrollmentModel());
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task UpdateEnrollmentAsync_ReturnsOk()
        {
            _enrollmentServicesMock.Setup(service => service.UpdateEnrollment(It.IsAny<Enrollment>(), It.IsAny<int>())).Returns(Task.CompletedTask);

            _enrollmentController = new EnrollmentsController(_enrollmentServicesMock.Object);

            var result = await _enrollmentController.EditPost(new EnrollmentModel(), 1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task DeleteEnrollmentAsync_ReturnsOk()
        {
            _enrollmentServicesMock.Setup(service => service.DeleteEnrollment(It.IsAny<int>())).Returns(Task.CompletedTask);

            _enrollmentController = new EnrollmentsController(_enrollmentServicesMock.Object);

            var result = await _enrollmentController.DeleteConfirmed(1);
            var obj = result as StatusCodeResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        



    }
}
