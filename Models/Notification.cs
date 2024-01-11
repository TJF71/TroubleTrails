using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TroubleTrails.Models
{
    public class Notification
    {
        int Id { get; set; }    // PK

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // FK which references the ticket table

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } // title of the notification

        [Required]
        [DisplayName("Message")]
        public string Message { get; set; } // message of the notification

        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTimeOffset Created { get; set; } // when the notification was created

        [Required]
        [DisplayName("Recipient")]
        public string RecipientId { get; set; } // FK which references the user table

        [Required]
        [DisplayName("Sender")]
        public string SenderId { get; set; } // FK which references the user table

        [DisplayName("Has been Viewed")]
        public bool Viewed { get; set; } // if the notification has been viewed or not true or false

        //Navigation Properties
        public virtual Ticket Ticket { get; set; } // navigation property to the ticket table
        public virtual BTUser Recipient { get; set; } // navigation property to the user table
        public virtual BTUser Sender { get; set; } // navigation property to the user table

    }
}
