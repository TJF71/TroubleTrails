using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace TroubleTrails.Models
{
    public class Project
    {

        public int Id { get; set; } // PK

        [DisplayName("Company")]
        public int? CompanyId { get; set; } // FK which references the company table

        [Required]  // required field
        [StringLength(50)]
        [DisplayName("Project Name")]
        public string Name { get; set; }  // name of the project

        [DisplayName("Description")]
        public string Description { get; set; } // description of the project

        [DisplayName("Start Date")]
        public DateTimeOffset StartDate { get; set; } // start date of the project

        [DisplayName("End Date")]
        public DateTimeOffset EndDate { get; set;}  //  end date of the project

        [DisplayName("Priority")]
        public int? ProjectPriorityId { get; set; } // FK which references the project priority table. Can be null


        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFormFile { get; set; } // the image file that is being uploaded

        [DisplayName("File Name")]
        public string ImageFileName { get; set; } // the name of the image file that is being uploaded 

        public byte[] ImageFileData { get; set; } // the data of the image file that is being uploaded

        [DisplayName("File Extension")]
        public string ImageContentType { get; set; } // image type 

        public bool Archived { get; set; } // if the project is archived or not 'true' or 'false


        //Navigation Properties
        public virtual Company Company { get; set; } // navigation property to the company table

        public virtual ProjectPriority ProjectPriority { get; set; } // navigation property to the project priority table

        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();  // navigation property to the member table

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();  // navigation property to the tickets table

    }
}
