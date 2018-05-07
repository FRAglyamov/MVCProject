using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVCProject.Contexts
{
    public class AppDbContext : IdentityDbContext<Student>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Work> Works { get; set; }
    }
}
