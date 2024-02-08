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
            _context = context;  // set the dbcontext to the private field
        
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
                                                .ThenInclude(t => t.Attachments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.History)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Notifications)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.DeveloperUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.OwnerUser)
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
            List<Project> projects = new(); 

            projects = await GetAllProjectsAsync(companyId); // get all the projects for the company

            result = projects.SelectMany(p => p.Tickets).ToList(); // select all the tickets from all the projects and convert to list 

            return result;  
        }

        public async Task<Company> GetCompanyInfoByIdAsync(int? companyId)
        {
            Company result = new(); // constructor for a new Company

        if (companyId != null) // if the company id is null
            {
                result = await _context.Companies
                                        .Include(c =>c.Members)  
                                        .Include(c => c.Projects)  
                                        .Include(c => c.Invites)
                                        .FirstOrDefaultAsync(c => c.Id == companyId); // get the company by its id
                  
            
            }
        return result;



            
        }
    }
}
