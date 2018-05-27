using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class TeacherTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string TeacherId { get; set; }
        public string Deadline { get; set; } 
        public string UnNormilizeDeadline { get; set; }
    }
}
