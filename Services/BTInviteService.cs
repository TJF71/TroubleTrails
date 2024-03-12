using Microsoft.EntityFrameworkCore;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTInviteService : IBTinviteService
    {
        private readonly ApplicationDbContext _context;

        public BTInviteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AcceptInviteAsync(Guid? token, string userId, int companyId)
        {
            Invite? invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);

            if(invite == null)
            {
                return false;
            }

            try
            {
                // cancel the invite; assign to the user and save
                invite.IsValid = false;
                invite.InviteeId = userId;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task AddNewInviteAsync(Invite invite)
        {
            try
            {
                await _context.Invites.AddAsync(invite);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AnyInviteAsync(Guid token, string email, int companyId)
        {
            try
            {   // see if we can find the result base on the three parameters sent
                bool result = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                    .AnyAsync(i => i.CompanyToken == token && i.InviteeEmail == email);
                return result;                                    
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        // these overloaded methods allow for the return of "invite" but allow for two cases of parameters
        public async Task<Invite> GetInviteAsync(int inviteId, int companyId)
        {
            try
            {
                Invite? invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                      .Include(i => i.Company)
                                                      .Include(i => i.Project)
                                                      .Include(i => i.Invitor)
                                                      .FirstOrDefaultAsync(i => i.Id == inviteId);
               
                return invite;
            }
            catch (Exception)
            {

                throw;
            }
        }
                   
        public async Task<Invite> GetInviteAsync(Guid token, string email, int companyId)   //Overloaded Method
        {
            try
            {                   // check the database 
                Invite? invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                      .Include(i => i.Company)
                                      .Include(i => i.Project)
                                      .Include(i => i.Invitor)
                                      .FirstOrDefaultAsync(i => i.CompanyToken == token && i.InviteeEmail == email);
                
                return invite;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidateInviteCodeAsync(Guid? token)
        {
            if(token == null)
            {
                return false;
            }

            bool result = false;

            Invite invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);

            if(invite != null)
            {
                //Determine invite date
                DateTime inviteDate = invite.InviteDate.DateTime;

                // Custom validation of invite based on the date it was issued
                // In this case we aere allownig an invite to e valid for 7 days

                bool validDate = (DateTime.Now - inviteDate).TotalDays <= 7;

                if(validDate)
                {
                    result = invite.IsValid;
                }

            }
            return result;
        }
    }
}
