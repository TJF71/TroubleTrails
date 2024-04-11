using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TroubleTrails.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; } // PK

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // FK wich references the ticket table

        [DisplayName("File Date")]
        public DateTimeOffset Created { get; set; } // when the attachment was created

        [DisplayName("Team Member")]   
        public string? UserId { get; set; } // FK which references the user table

        [DisplayName("File Description")]
        public string? Description { get; set; }  



        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; } // the file that is being uploaded

        [DisplayName("File Name")]
        public string? FileName { get; set; } // the name of the file that is being uploaded 

        public byte[]? FileData { get; set; } // the data of the file that is being uploaded

        [DisplayName("File Extension")]
        public string? FileContentType { get; set; }  //   

        //Navigation Properties
        public virtual Ticket? Ticket { get; set; } // navigation property to the ticket table   
        public virtual BTUser? User { get; set; } // navigation property to the user table       
    }
}
