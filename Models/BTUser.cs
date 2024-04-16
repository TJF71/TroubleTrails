using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace TroubleTrails.Models
{
    public class BTUser : IdentityUser //btuser inherits from identityuser and extends it with the properties below
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }  //concatenates first and last name from db 


        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? AvatarFormFile { get; set; }

        [DisplayName("Avatar")]
        public string? AvatarFileName { get; set; }
        public byte[]? AvatarFileData { get; set; } 

        [Display(Name = "File Extension")]
        public string? AvatarContentType { get; set; } 

       public int CompanyID { get; set; } //FK to the company table

        //Navigation Properties
        public virtual Company? Company { get; set; } // navigation property to the company table

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();// join table between btuser and project

    }
}
