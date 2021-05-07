using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Departmentcrs
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Department")]
        public int? DeptId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public Department Department { get; set; }
        public Course Course { get; set; }
    }
}