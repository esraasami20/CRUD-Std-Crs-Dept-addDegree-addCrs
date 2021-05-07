using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class studentcrs
    {
        [Key]
        [Column(Order =0)]
        [ForeignKey("student")]
        public int Id { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public int? Degree { get; set; }
        public Student student { get; set; }
        public Course Course { get; set; }

    }
}