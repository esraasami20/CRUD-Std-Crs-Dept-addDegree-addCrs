using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Department
    {
        [Key]
        public int? DeptId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string DeptName { get; set; }
        public virtual List<Student> Students { get; set; }
        public virtual List<Departmentcrs> departmentcrs { get; set; }
    }
}