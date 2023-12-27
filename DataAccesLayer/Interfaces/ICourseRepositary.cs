using DataAccessLayer.Entity;


namespace DataAccessLayer.Interfaces
{
    public interface ICourseRepositary
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course, int id);
        Task DeleteCourseAsync(int id);
        

    }
}
