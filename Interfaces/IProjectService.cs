using BridgeRTU.Domain;
using BridgeRTU.Services.Activities.Dto;
using BridgeRTU.Services.Projects.Dto;

namespace BridgeRTU.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDetailsDto>> GetAllProjectsAsync();
        Task<List<Project>> GetRecommendedProjects(int userId);
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(int id, Project project);
        Task<bool> DeleteProjectAsync(int id);
    }
}
