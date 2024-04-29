﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TroubleTrails.Extensions;

namespace TroubleTrails.Models
{
    public class TicketAttachment
    {
        private DateTimeOffset _created;


        public int Id { get; set; } // PK

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // FK wich references the ticket table

        //[DisplayName("File Date")]
        //public DateTimeOffset Created { get; set; } // when the attachment was created


        [DisplayName("File Date")]
        public DateTimeOffset Created
        {
            get => _created;
            set => _created = value.ToUniversalTime();
        }





        [DisplayName("Team Member")]   
        public string? UserId { get; set; } // FK which references the user table

        [DisplayName("File Description")]
        public string? Description { get; set; }



        [NotMapped]
        [DisplayName("Select a file")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".ppt", ".pptx", ".html"})]
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
