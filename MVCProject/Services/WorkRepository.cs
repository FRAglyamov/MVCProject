using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Contexts;
using MVCProject.Models;

namespace MVCProject.Services
{
    public class WorkRepository : Repository<Work>
    {
        private readonly AppDbContext _context;

        public WorkRepository(AppDbContext context) : base(context.Works)
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
