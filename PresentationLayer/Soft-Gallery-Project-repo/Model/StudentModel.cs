using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PresentationLayer.Models
{
    public class StudentModel
    {
        public int ID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstMidName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime JoinedDate { get; set; }

        public List<EnrollmentModel> Enrollments { get; set; }
    }

    
  
}
