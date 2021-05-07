using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Course
    {
        [Key]
        public int CrsId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CrsName { get; set; }
        public virtual List<studentcrs> Studentcrs { get; set; }
        public virtual List<Departmentcrs> Departmentcrs { get; set; }
    }
}