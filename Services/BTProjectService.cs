using Microsoft.EntityFrameworkCore;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Models.Enums;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTProjectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _rolesService;  // underscore is a naming convention for private fields

        public BTProjectService(ApplicationDbContext context, IBTRolesService rolesService)
        {
            _context = context;
            _rolesService = rolesService;
        }

        // CRUD - Create 
        public async Task AddNewProjectAsync(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync(); // save the changes to the database
        }

        public async Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {
            BTUser currentPM = await GetProjectManagerAsync(projectId);

            // Remove the current project manager if necessary
            if (currentPM != null)
            {
                try
                {
                    await RemoveProjectManagerAsync(projectId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"**** ERROR **** - Error Removing New Project Manager.  --> {ex.Message}");
                    return false;
                }
            }

            try
            {
                await AddUserToProjectAsync(userId, projectId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Error Adding Project Manager.  --> {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {
            BTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId); // get the user by id

            if (user != null)
            {
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id
                if (!await IsUserOnProjectAsync(userId, projectId)) // check if the user is not on the project
                {
                    try
                    {
                        project.Members.Add(user); // add the user to the project
                        await _context.SaveChangesAsync(); // save the changes to the database
                        return true; // return true if the user is added to the project
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }

            }
            else
            {
                return false; // return false if the user is not found  
            }
            {
                return false; // return false for any other reason
            }

        }

        // CRUD = Archive (Delete)
        public async Task ArchiveProjectAsync(Project project)
        {
            try
            {
                project.Archived = true; // set the project to archived
                await UpdateProjectAsync(project); // update the project

                //Archive all the tickets in the project
                foreach (Ticket ticket in project.Tickets)
                {
                    ticket.ArchivedByProject = true; // set the ticket to archived
                    _context.Update(ticket); // update the ticket
                    await _context.SaveChangesAsync(); // save the changes to the database
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(int projectId)
        {
            List<BTUser> developers = await GetProjectMembersByRoleAsync(projectId, Roles.Developer.ToString());
            List<BTUser> submitters = await GetProjectMembersByRoleAsync(projectId, Roles.Submitter.ToString());
            List<BTUser> admins = await GetProjectMembersByRoleAsync(projectId, Roles.Admin.ToString());

            List<BTUser> teamMembers = developers.Concat(submitters).Concat(admins).ToList();

            return teamMembers;
        }

        public async Task<List<Project>> GetAllProjectsByCompanyAsync(int companyId)
        {
            List<Project> projects = new();

            projects = await _context.Projects.Where(p => p.CompanyId == companyId && p.Archived == false)
                                            // eager loading
                                            .Include(p => p.Members)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Comments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Attachments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.History)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Notifications)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.DeveloperUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.OwnerUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketStatus)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketType)
                                            .Include(p => p.ProjectPriority)
                                            .ToListAsync();
            return projects;

        }

        public async Task<List<Project>> GetAllProjectsByPriority(int companyId, string priorityName)
        {
            List<Project> projects = await GetAllProjectsByCompanyAsync(companyId); // get all the projects by company id
            int priorityId = await LookupProjectPriorityId(priorityName); // get the priority id by the priority name

            return projects.Where(p => p.ProjectPriorityId == priorityId).ToList(); // return the projects where the project priority id is equal to the priority id


        }

        public async Task<List<Project>> GetArchivedProjectsByCompanyAsync(int companyId)
        {
            List<Project> projects = await _context.Projects.Where(p => p.CompanyId == companyId && p.Archived == true)
                                            // eager loading
                                            .Include(p => p.Members)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Comments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Attachments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.History)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Notifications)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.DeveloperUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.OwnerUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketStatus)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketType)
                                            .Include(p => p.ProjectPriority)
                                            .ToListAsync();

            return projects; // return the projects
        }

        public Task<List<BTUser>> GetDevelopersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        // CRUD - Read
        public async Task<Project> GetProjectByIdAsync(int projectId, int companyId)
        {
            //Project? project = await _context.Projects
            //                                .Include(p => p.Tickets)
            //                                .Include(p => p.Members)
            //                                .Include(p => p.ProjectPriority)
            //                                .FirstOrDefaultAsync(p => p.Id == projectId && p.CompanyId == companyId); // get the project by id and company id  

            Project? project = await _context.Projects
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)
                                             .Include(p => p.Tickets)
                                                    .ThenInclude(t => t.TicketStatus)
                                             .Include(p => p.Tickets)
                                                    .ThenInclude(t => t.TicketType)
                                             .Include(p => p.Tickets)
                                                     .ThenInclude(t => t.DeveloperUser)
                                             .Include(p => p.Tickets)
                                                     .ThenInclude(t => t.OwnerUser)
                                             .Include(p => p.Members)
                                             .Include(p => p.ProjectPriority)
                                            .FirstOrDefaultAsync(p => p.Id == projectId && p.CompanyId == companyId); // get the project by id and company id

            return project;

        }

        public async Task<BTUser> GetProjectManagerAsync(int projectId)
        {
            Project? project = await _context.Projects
                                             .Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id

            foreach (BTUser member in project?.Members)  // loop through the members of the project  ? is a null conditional operator
            {
                if (await _rolesService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString())) // check if the user is in the role of project manager
                {
                    return member; // return the member
                }
            }

            return null; // return null if the member is not found

        }

        public async Task<List<BTUser>> GetProjectMembersByRoleAsync(int projectId, string role)
        {
            Project? project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id

            List<BTUser> members = new(); // create a new list of members

            foreach (var user in project.Members)
            {
                if (await _rolesService.IsUserInRoleAsync(user, role))
                {
                    members.Add(user); // add the user to the list of members
                }
            }

            return members;

        }

        public Task<List<BTUser>> GetSubmittersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        #region Get Unassigned Projects
        public async Task<List<Project>> GetUnassignedProjectsAsync(int companyId)
        {
            List<Project> result = new();
            List<Project> projects = new();

            try
            {
                projects = _context.Projects
                                    .Include(p => p.ProjectPriority)
                                    .Where(p=>p.CompanyId == companyId)
                                    .ToList(); // get the projects by company id 

                foreach (Project project in projects)
                {
                    if ((await GetProjectMembersByRoleAsync(project.Id, nameof(Roles.ProjectManager))).Count == 0)   //if there are no project managers
                    {
                        result.Add(project); // add the project to the result
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result; // return the result
        }
        #endregion 

        public async Task<List<Project>> GetUserProjectsAsync(string userId)
        {
            try
            {
                List<Project> userProjects = (await _context.Users
                     .Include(u => u.Projects)
                         .ThenInclude(p => p.Company)
                     .Include(u => u.Projects)
                         .ThenInclude(p => p.Members)
                     .Include(u => u.Projects)
                         .ThenInclude(p => p.Tickets)
                     .Include(u => u.Projects)
                         .ThenInclude(t => t.Tickets)
                             .ThenInclude(t => t.DeveloperUser)
                     .Include(u => u.Projects)
                         .ThenInclude(t => t.Tickets)
                             .ThenInclude(t => t.OwnerUser)
                      .Include(u => u.Projects)
                          .ThenInclude(t => t.Tickets)
                             .ThenInclude(t => t.TicketPriority)
                      .Include(u => u.Projects)
                          .ThenInclude(t => t.Tickets)
                              .ThenInclude(t => t.TicketStatus)
                     .Include(u => u.Projects)
                             .ThenInclude(t => t.Tickets)
                                 .ThenInclude(t => t.TicketType)
                     .FirstOrDefaultAsync(u => u.Id == userId)).Projects.ToList();

                return userProjects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Error Removing User project list.  --> {ex.Message}");
                throw;
            }

        }


        public async Task<List<BTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId)
        {
            List<BTUser> users = await _context.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToListAsync();  // get the users where the project id is not equal to the project id

            return users.Where(u => u.CompanyID == companyId).ToList(); // return the users where the company id is equal to the company id

        }

        
        public async Task<bool> IsAssignedProjectManagerAsync(string userId, int projectId)
        {
            try
            {
                string projectManagerId = (await GetProjectManagerAsync(projectId))?.Id;

                if(projectManagerId == userId)
                {
                   return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        
        public async Task<bool> IsUserOnProjectAsync(string userId, int projectId)
        {
            Project project = await _context.Projects.Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id

            bool result = false;

            if (project != null)
            {
                result = project.Members.Any(m => m.Id == userId); // check if the user is on the project
            }

            return result;
        }

        public async Task<int> LookupProjectPriorityId(string priorityName)
        {
            int priorityId = (await _context.ProjectPriorities.FirstOrDefaultAsync(p => p.Name == priorityName)).Id; // get the priority id by the priority name
            return priorityId;
        }

        public async Task RemoveProjectManagerAsync(int projectId)
        {
            Project? project = await _context.Projects
                                            .Include(p => p.Members)
                                            .FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id
            try
            {
                foreach (BTUser member in project?.Members)
                {
                    if (await _rolesService.IsUserInRoleAsync(member, Roles.ProjectManager.ToString()))
                    {
                        await RemoveUserFromProjectAsync(member.Id, projectId);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task RemoveUserFromProjectAsync(string userId, int projectId)
        {
            try
            {
                BTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId); // get the user by id
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId); // get the project by id

                try
                {
                    if (await IsUserOnProjectAsync(userId, projectId))
                    {
                        project.Members.Remove(user);      // remove the user from the project
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw; // deep dive into the exception from database
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Error Removing User from project.  --> {ex.Message}");
            }

        }

        public async Task RemoveUsersFromProjectByRoleAsync(string role, int projectId)
        {
            try
            {
                List<BTUser> members = await GetProjectMembersByRoleAsync(projectId, role);
                Project? project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                foreach (BTUser btUser in members)
                {
                    try
                    {
                        project.Members.Remove(btUser);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw; // throw the exception onto the stack
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Error Removing User from project.  --> {ex.Message}");
                throw;
            }


        }

        public async Task RestoreProjectAsync(Project project)
        {
            try
            {
                project.Archived = false; // set the project to not archived
                await UpdateProjectAsync(project); // update the project

                //Archive all the tickets in the project
                foreach (Ticket ticket in project.Tickets)
                {
                    ticket.ArchivedByProject = false; // set the ticket to not archived
                    _context.Update(ticket); // update the ticket
                    await _context.SaveChangesAsync(); // save the changes to the database
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // CRUD - Update
        public async Task UpdateProjectAsync(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
