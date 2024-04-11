using System.ComponentModel;

namespace TroubleTrails.Models
{
    public class ProjectPriority
    {

        public int Id { get; set; } // PK

        [DisplayName("Priority Name")]
        public string? Name { get; set; }
    }
}
