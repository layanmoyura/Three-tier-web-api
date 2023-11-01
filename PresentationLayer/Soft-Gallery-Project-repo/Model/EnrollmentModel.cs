
namespace PresentationLayer.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class EnrollmentModel
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        


    }
}
