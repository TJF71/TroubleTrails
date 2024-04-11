using System.ComponentModel;
using System.Drawing.Printing;

namespace TroubleTrails.Models
{
    public class TicketHistory
    {

        public int Id { get; set; } // PK

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // FK wich references the ticket table

        [DisplayName("Updated Item")]
        public string? Property { get; set; } // property that was changed

        [DisplayName("Previous")]
        public string? OldValue { get; set; } // old value of the property

        [DisplayName("Current")]  
        public string? NewValue { get; set; } // new value of the property

        [DisplayName("Date Modified")]
        public DateTimeOffset Created { get; set; } // when the change was made 

        [DisplayName("Description of Change")]
        public string? Description { get; set; } // description of the change

        [DisplayName("Team Member")]
        public string? UserId { get; set; } // FK which references the user table

        //Navigation Properties
        public virtual Ticket? Ticket { get; set; } // navigation property to the ticket table

        public virtual BTUser? User { get; set; } // navigation property to the user table
    }
}
