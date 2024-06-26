﻿using TroubleTrails.Models;

namespace TroubleTrails.Services.Interfaces
{
    public interface IBTLookupService
    {
        public Task<List<TicketPriority>> GetTicketPrioritiesAsync();
        public Task<List<TicketStatus>> GetTicketStatusesAsync();
        public Task<List<TicketType>> GetTicketTypesAsync();
        public Task<List<ProjectPriority>> GetProjectPrioritiesAsync();
    }
}
