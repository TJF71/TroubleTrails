using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TroubleTrails.Models
{
    public class Ticket
    {
        public int Id { get; set; } // Primary Key   

        private DateTimeOffset _created;
        private DateTimeOffset? _updated;

        [DisplayName("Created")]
        public DateTimeOffset Created
        {
            get => _created;
            set => _created = value.ToUniversalTime();
        }
       

        [DisplayName("Updated")]
        public DateTimeOffset? Updated
        {
            get => _updated;
            set => _updated = value?.ToUniversalTime();
        }

        [Required]                  //data annotation
        [StringLength(50)]          //data annotation
        [DisplayName("Title")]      //data annotation
        public string Title { get; set; } // Title of the ticket (property)

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; } // Description of the ticket.  No text limit   

        //[DataType(DataType.Date)]        
        //[DisplayName("Created")]
        //public DateTimeOffset Created { get; set; } // When the ticket was created

        //[DataType(DataType.Date)]
        //[DisplayName("Updated")]
        //public DateTimeOffset Updated { get; set; } // When the ticket was last updated.  Can be null(?)

        [DisplayName("Archived")]
        public bool Archived { get; set; } // Is the ticket archived?


        // Foreign Keys
        [DisplayName("Project")]
        public int ProjectId { get; set; }  // Foreign Key to the Project table

        [DisplayName("Ticket Type")]
        public int TicketTypeId { get; set; } // Foreign Key to the TicketType table

        [DisplayName("Ticket Priority")]
        public int TicketPriorityId { get; set; } // Foreign Key to the TicketPriority table    

        [DisplayName("Ticket Status")]
        public int TicketStatusId { get; set; } // Foreign Key to the TicketStatus table

        [DisplayName("Ticket Owner")]
        public string? OwnerUserId { get; set; } // Foreign Key to the User table\
                                                // not integer because it is a BTUser    

        [DisplayName("Ticket Developer")]
        public string? DeveloperUserId { get; set; } // Foreign Key to the User table
                                                    // not integer because it is a BTUser
                                                
        // Navigation Properties
        public virtual Project? Project { get; set; } // navigation property to the Project table    

        public virtual TicketType? TicketType { get; set; } // navigation property to the TicketType table

        public virtual TicketPriority? TicketPriority { get; set; } // navigation property to the TicketPriority table

        public virtual TicketStatus? TicketStatus { get; set; } // navigation property to the TicketStatus table

        public virtual BTUser? OwnerUser { get; set; } // navigation property to the User table

        public virtual BTUser? DeveloperUser { get; set; } // navigation property to the User table  


        // Navigation Properties for collections. One ticket can have many comments, attachments, and notifications
        // virtual allows us to use lazy loading
        // lazy loading is when we load the data when we need it
        public virtual ICollection<TicketComment> Comments { get; set; } = new HashSet<TicketComment>(); // navigation property to the TicketComment table
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new HashSet<TicketAttachment>(); // navigation property to the TicketAttachment table
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); // navigation property to the Notification table

        public virtual ICollection<TicketHistory> History { get; set; } = new HashSet<TicketHistory>(); // navigation property to the TicketHistory table   


        
    
    }
}
