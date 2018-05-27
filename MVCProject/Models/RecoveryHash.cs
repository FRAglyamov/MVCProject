using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class RecoveryHash
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Magic { get; set; }
        public string UserId { get; set; }
    }
}
