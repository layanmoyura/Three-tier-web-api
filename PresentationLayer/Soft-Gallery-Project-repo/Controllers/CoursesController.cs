using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {   
        
        private readonly ICourseServices _courseServices;

        public CoursesController( ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [Route("index")]
        [HttpGet]
        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var course = await _courseServices.GetCoursesAsync();
            var coursemodels = MappingFunctions.ToCourseModelList(course);
            return Ok(coursemodels);
        }

        // GET: Courses/Details/5
        [Route("details/{id}")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseServices.GetCourseByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            var coursemodel = MappingFunctions.ToCourseModel(course);

            return Ok(coursemodel);
        }


        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Create([Bind("CourseID,Title,Credits")] CourseModel coursemodel)
        {     
                var course = MappingFunctions.ToCourse(coursemodel);
                await _courseServices.AddCourseAsync(course);
                return RedirectToAction(nameof(Index));

        }

        // GET: Courses/Edit/5


        [Route("update/{id}")]
        [HttpPut]
        public async Task<ActionResult> EditPost([Bind("CourseID,Title,Credits")] CourseModel courseModel, int id)
        {
           
            var updatecourse = MappingFunctions.ToCourse(courseModel);
            await _courseServices.UpdateCourseAsync(updatecourse,id);
            return RedirectToAction(nameof(Index));
                
            
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            try
            {
                var course = await _courseServices.GetCourseByIdAsync(id.Value);
                await _courseServices.DeleteCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }

            catch(DbUpdateException)
            {
                return BadRequest("Delete the existing Enrollments");
            }
            
        }
    }
}
