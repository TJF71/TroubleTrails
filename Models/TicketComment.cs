﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TroubleTrails.Models
{
    public class TicketComment
    {
        private DateTimeOffset _created;

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created 
        {
            get => _created;
            set => _created = value.ToUniversalTime();
        }

        public int Id { get; set; }



        [DisplayName("Member Comment")]
        public string? Comment { get; set; }  // the comment itself

   //     [DisplayName("Date")]
     //   public DateTimeOffset Created { get; set; } // when the comment was made

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // FK which references the ticket table   

        [DisplayName("Team Member")]
        public string? UserId { get; set; } // FK which references the user table

        //--Navigation Properties--//

        public virtual Ticket? Ticket { get; set; } // navigation property to the ticket table   
                                                    // gives us access to the properties of the ticket table

        public virtual BTUser? User { get; set; } // navigation property to the user table   



    }
}
