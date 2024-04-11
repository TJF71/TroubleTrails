using System.ComponentModel;

namespace TroubleTrails.Models
{
    public class Invite
    {
        public int Id { get; set; } // PK

        [DisplayName("Date Sent")]
        public DateTimeOffset InviteDate { get; set; }   // when the invite was sent

        [DisplayName("Join Date")]
        public DateTimeOffset JoinDate { get; set; } // when the user joined the project

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; } // token for the company  globally unique identifier

      
        [DisplayName("Company")]
        public int? CompanyId { get; set; } // FK which references the company table

        [DisplayName("Project")]
        public int? ProjectId { get; set; } // FK which references the project table

        [DisplayName("Invitor")]
        public string? InvitorId { get; set; } // FK which references the Id of a user

        [DisplayName("Invitee")]
        public string? InviteeId { get; set; } // FK which references the Id of a user

        [DisplayName("Invitee Email")]
        public string? InviteeEmail { get; set; } // email of the invitee

        [DisplayName("Invitee First Name")]
        public string? InviteeFirstName { get; set; } // first name of the invitee
        
        [DisplayName("Invitee Last Name")]
        public string? InviteeLastName { get; set; } // last name of the invitee

        public bool IsValid { get; set; } // if the invite is valid or not true or false

        //Navigation Properties

        public virtual Company? Company { get; set; } // navigation property to the company table
        public virtual BTUser? Invitor { get; set; } // navigation property to the user table
        public virtual BTUser? Invitee { get; set; } // navigation property to the user table
        public virtual Project? Project { get; set; } // navigation property to the project table

    }
}
