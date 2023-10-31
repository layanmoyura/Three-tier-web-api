using DataAccessLayer.Entity;


namespace BusinessLayer.Interfaces
{
    public interface ICourseServices
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course, int id);
        Task DeleteCourseAsync(Course course);
    }
}
