using Microsoft.AspNetCore.Identity.UI.Services;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTNotification : IBTNotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IBTRolesService _rolesService;

        public BTNotification(ApplicationDbContext context,
                             IEmailSender emailSender,
                             IBTRolesService rolesService)
        {
            _context = context;
            _emailSender = emailSender;
            _rolesService = rolesService;
        }

        Task IBTNotificationService.AddNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        Task<List<Notification>> IBTNotificationService.GetRecievedNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        Task<List<Notification>> IBTNotificationService.GetSentNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        Task IBTNotificationService.SendEmailNotificationAsync(Notification notification, string emailSubject)
        {
            throw new NotImplementedException();
        }

        Task IBTNotificationService.SendEmailNotificationsByRoleAsync(Notification notification, int companyId, string role)
        {
            throw new NotImplementedException();
        }

        Task IBTNotificationService.SendMembersEmailNotificationsAsync(Notification notification, List<BTUser> members)
        {
            throw new NotImplementedException();
        }
    }
}
