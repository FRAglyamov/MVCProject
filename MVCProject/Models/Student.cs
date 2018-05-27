using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVCProject.Models
{
    public class Student : IdentityUser
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Sex { get; set; }
        public DateTime Birthdate { get; set; }
        public int EmailChecher { get; set; }
        //public int GroupId { get; set; }
        //public int IsEmailConfirmed { get; set; } = 0;
    }
}
