namespace TroubleTrails.Models
{
    public class TicketComment
    {
        //Id
        public int Id { get; set; }

        //Comment

        public string Comment { get; set; }

        //Created

        public DateTimeOffset Created { get; set; } // when the comment was made

        //TicketId

        public int TicketId { get; set; } // FK which references the ticket table   

        //UserId

        public string UserId { get; set; } // FK which references the user table

        //--Navigation Properties--//

        //Ticket
        public virtual Ticket Ticket { get; set; } // navigation property to the ticket table   
        //User
        public virtual BTUser User { get; set; } // navigation property to the user table   



    }
}
