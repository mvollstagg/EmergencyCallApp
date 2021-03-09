using Microsoft.EntityFrameworkCore;
using EmergencyCall.Entities;
using EmergencyCall.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmergencyCall.Data.DAL;

namespace EmergencyCall.Data.Repositories
{
    public class HelpResponseRepository : Repository<HelpResponse>, IHelpResponseRepository
    {
        public HelpResponseRepository(ApplicationDbContext context) : base(context) { }
        
        

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}