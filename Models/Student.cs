using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50,MinimumLength =2)]
        public string Name { get; set; }
        [Range(10,20)]
        public int Age { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        public string Email { get; set; }
        public int password { get; set; }
        [Compare("password")]
        [NotMapped]
        public int Cpassord { get; set; }
        public string studentimg { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department Department { get; set; }

        public List<studentcrs> Studentcrs { get; set; }

    }
}