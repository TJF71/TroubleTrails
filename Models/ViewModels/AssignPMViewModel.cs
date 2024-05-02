using Microsoft.AspNetCore.Mvc.Rendering;

namespace TroubleTrails.Models.ViewModels
{
    public class AssignPMViewModel
    {

        public Project project { get; set; }

        public SelectList PMList { get; set; }

        public string PMID { get; set; }


    }
}
