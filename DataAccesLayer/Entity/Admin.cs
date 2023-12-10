
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Admin
    {
        [Key]
        public int UserID { get; set; }


        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
