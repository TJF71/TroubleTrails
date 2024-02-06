using TroubleTrails.Models;

namespace TroubleTrails.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
       public Task<Company> GetCompanyInfoByIdAsync(int? companyId); // first method to get company info by id ? = nullable
        public Task<List<BTUser>> GetAllMemebersAsync(int companyId); // second method to get all members of a company
        public Task<List<Project>> GetAllProjectsAsync(int companyId);
        public Task<List<Ticket>> GetAllTicketsAsync(int companyId); // third method to get all tickets of a company
    }
}
