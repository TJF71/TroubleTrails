using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TroubleTrails.Models;

namespace TroubleTrails.Data
{
    public class ApplicationDbContext : IdentityDbContext<BTUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet is a collection of entities that can be queried from the database
        // each entity corresponds to a table or view in the database
        public DbSet<Company> Companies { get; set; } // Companies table
        public DbSet<Invite> Invites { get; set; } // Invites table
        public DbSet<Project> Projects { get; set; } // Projects table
        public DbSet<Ticket> Tickets { get; set; } // Tickets table
        public DbSet<Notification> Notifications { get; set; } // Notifications table
        public DbSet<ProjectPriority> ProjectPriorities { get; set; } // ProjectPriorities table
        public DbSet<TicketAttachment> TicketAttachments { get; set; } // TicketAttachments table
        public DbSet<TicketComment> TicketComments { get; set; } // TicketComments table
        public DbSet<TicketHistory> TicketHistories { get; set; } // TicketHistories table
        public DbSet<TicketPriority> TicketPriorities { get; set; } // TicketPriorities table
        public DbSet<TicketStatus> TicketStatuses { get; set; } // TicketStatuses table
        public DbSet<TicketType> TicketTypes { get; set; } // TicketTypes table
    }
}