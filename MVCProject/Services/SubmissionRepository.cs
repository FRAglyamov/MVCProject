using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Contexts;
using MVCProject.Models;

namespace MVCProject.Services
{
    public class SubmissionRepository : Repository<Submission>
    {
        private readonly AppDbContext _context;

        public SubmissionRepository(AppDbContext context) : base(context.Submissions)
        {
            _context = context;
        }
        public override void Save()
        {
            using (_context)
            {
                _context.SaveChanges();
            }
        }
    }
}
