
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entity
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        public DateTime? JoinedDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}