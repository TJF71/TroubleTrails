using System.ComponentModel;

namespace TroubleTrails.Models
{
    public class Company
    {

        public int Id { get; set; } // PK

        [DisplayName("Company Name")]
        public string Name { get; set; } // name of the company

        [DisplayName("Company Description")]
        public string Description { get; set; } // description of the company

        //Navigation Properties
        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>(); // navigation property to the BTUser table  lazy loading

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>(); // navigation property to the project table lazy loading

        // create relationship to Invites
        public virtual ICollection<Invite> Invites { get; set; } = new HashSet<Invite>(); // navigation property to the invite table lazy loading



    }
}
