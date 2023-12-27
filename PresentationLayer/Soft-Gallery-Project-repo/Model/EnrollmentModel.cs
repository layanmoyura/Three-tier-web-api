

using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class EnrollmentModel
    {
        public int EnrollmentID { get; set; }

        [Required]
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public Grade Grade { get; set; }

        public StudentModel Student { get; set; }

        public CourseModel Course { get; set; }





       
        


    }
}
