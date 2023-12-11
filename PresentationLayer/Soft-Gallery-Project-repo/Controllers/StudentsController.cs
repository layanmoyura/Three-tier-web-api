﻿using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Threading.Tasks;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentServices studentServices;

        public StudentsController(IStudentServices studentServices)
        {
            this.studentServices = studentServices;            
        }

        //READ
        [Authorize]
        [Route("index")]
        [HttpGet]
        
        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var students = await studentServices.GetStudentsAsync(sortOrder, searchString);

            var studentmodels = MappingFunctions.ToStudentModelList(students);


            return Ok(studentmodels);

        }

        [Route("details/{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentServices.GetStudentDetailsAsync(id.Value);

            var studentmodel = MappingFunctions.ToStudentModel(student);

            return Ok(studentmodel);
        }



        [Authorize]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Create([Bind("LastName,FirstMidName,JoinedDate")] StudentModel studentmodel)
        {
       
            var student = MappingFunctions.ToStudent(studentmodel);
            await studentServices.CreateStudentAsync(student);
            return RedirectToAction(nameof(Index));
            
        }


        [Route("update/{id}")]
        [HttpPut]
        public async Task<ActionResult> EditPost([Bind("LastName,FirstMidName,JoinedDate")] StudentModel studentmodel, int id)
        {
            var updatestudent = MappingFunctions.ToStudent(studentmodel);
            await studentServices.UpdateStudentAsync(updatestudent, id);
            return RedirectToAction(nameof(Index));
        }


        // POST: Students/Delete/5
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteConfirm(int? id)


        {
            try
            {
                await studentServices.DeleteStudentAsync(id.Value);
                return RedirectToAction(nameof(Index));
            }

            catch (DbUpdateException)
            {
                {
                    return BadRequest("Delete the existing Enrollments");
                }

            }
        }

    }
}
