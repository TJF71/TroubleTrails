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

        public Task<bool> AnyInviteAsync(Guid token, string email, int companyId)
        {

        }

        public Task<Invite> GetInviteAsync(int inviteId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<Invite> GetInviteAsync(Guid token, string email, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateInviteCodeAsync(Guid? token)
        {
            throw new NotImplementedException();
        }
    }
}
