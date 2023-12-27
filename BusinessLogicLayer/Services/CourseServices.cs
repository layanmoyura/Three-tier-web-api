using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;
using BusinessLayer.Interfaces;


public class CourseService : ICourseServices
{
    private readonly ICourseRepositary _repository;

    public CourseService(ICourseRepositary repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _repository.GetCoursesAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _repository.GetCourseByIdAsync(id);
    }

    public async Task AddCourseAsync(Course course)
    {
        await _repository.AddCourseAsync(course);
    }

    public async Task UpdateCourseAsync(Course course, int id)
    {
        await _repository.UpdateCourseAsync(course,id);
    }

    public async Task DeleteCourseAsync(int id)
    {
        await _repository.DeleteCourseAsync(id);
    }
}
