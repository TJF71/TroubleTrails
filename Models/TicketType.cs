using System.ComponentModel;

namespace TroubleTrails.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        [DisplayName("Type Name")]
        public string? Name { get; set; }    

    }
}
