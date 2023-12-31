﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataAccessLayer.Data;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : Controller
    {
        
        private readonly IEnrollmentService _enrollmentService;


        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            
            _enrollmentService = enrollmentService;
        }

        // GET: Enrollments
        [Authorize]
        [Route("index")]
        [HttpGet]
        public async Task <ActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollments();
            var enrollmentmodels = MappingFunctions.ToEnrollmentModelList(enrollments);
            return Ok(enrollmentmodels);
        }

        // GET: Enrollments/Details/5
        [Authorize]
        [Route("details/{id}")]
        [HttpGet]
        public async Task <ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            return Ok(enrollmentmodel);
        }

        [Authorize]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Create([Bind("CourseID,StudentID,Grade")] EnrollmentModel enrollmentmodel)
        {
           
            var enrollment = MappingFunctions.ToEnrollment(enrollmentmodel);
            await _enrollmentService.AddEnrollment(enrollment);
            return StatusCode(200);
           
        }



        // POST: Enrollments/Edit/5
        [Authorize]
        [Route("update/{id}")]
        [HttpPut]
        public async Task<ActionResult> EditPost([Bind("CourseID,StudentID,Grade")] EnrollmentModel enrollmentmodel, int id)
        {
            var updateenrollment = MappingFunctions.ToEnrollment(enrollmentmodel);
            await _enrollmentService.UpdateEnrollment(updateenrollment, id);
            return StatusCode(200);
            
        }


        // POST: Enrollments/Delete/5
        [Authorize]
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            await _enrollmentService.DeleteEnrollment(id.Value);
            return StatusCode(200);
        }

       
    }
}
