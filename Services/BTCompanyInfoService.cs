using Microsoft.EntityFrameworkCore;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        private readonly ApplicationDbContext _context;
        public BTCompanyInfoService(ApplicationDbContext context) 
        {
            _context = context;
        
        }
        public async Task<List<BTUser>> GetAllMemebersAsync(int companyId)
        {
            List<BTUser> result = new();  // constructor for a new list of BTUser

            result = await _context.Users.Where(u => u.CompanyID == companyId).ToListAsync();
            
            return result;
        }

        public async Task<List<Project>> GetAllProjectsAsync(int companyId)
        {
            List<Project> result = new(); // constructor for a new list of Project

            result = await _context.Projects.Where(p => p.CompanyId == companyId)
                                            // eager loading
                                            .Include(p => p.Members)  
                                            .Include(p => p.Tickets)  
                                                .ThenInclude(t => t.Comments)  
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketStatus)  
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)  
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketType) 
                                            .Include(p => p.ProjectPriority)  
                                            .ToListAsync(); // convert to list
            return result;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(int companyId)
        {
           List<Ticket> result = new(); // constructor for a new list of Ticket

            result = await _context.Tickets.Where(t => t.ProjectId == companyId).ToListAsync();

            return result;  
        }

        public async Task<Company> GetCompanyInfoByIdAsync(int? companyId)
        {
            Company result = new(); // constructor for a new Company

            result = await _context.Companies.FindAsync(companyId);

            return result;
            
        }
    }
}
