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
    public class HelpRequestRepository : Repository<HelpRequest>, IHelpRequestRepository
    {
        public HelpRequestRepository(ApplicationDbContext context) : base(context) { }
        
        

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<HelpRequest>> GetAllHelpRequests()
        {
            return await ApplicationDbContext.HelpRequests
                .Include(a => a.HelpResponses)
                    .ThenInclude(b => b.User)
                .Include(b => b.User)
                .ToListAsync();
        }
    }
}