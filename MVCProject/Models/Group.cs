using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int DisciplineId { get; set; }

    }
}
