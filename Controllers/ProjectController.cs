using System.Security.Claims;
using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Services.Projects.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BridgeRTU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectDetailsDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            //var projectDtos = projects.Select(p => new ProjectDetailsDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    Skills=p.Skills,
            //    FieldOfStudy=p.FieldOfStudy,
            //    Status = p.Status,
            //    CreatedAt = p.CreatedAt,
            //    UpdatedAt = p.UpdatedAt,
            //    StudentId = p.StudentId,
            //    Comments=p.Comments.ToList(),
            //    StudentName=p.Student.FullName
            //}).ToList();

            return Ok(projects);
        }
       

        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailsDto>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var projectDto = new ProjectDetailsDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                StudentId = project.StudentId
            };

            return Ok(projectDto);
        }

        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<ProjectDetailsDto>> CreateProject(ProjectDetailsDto projectDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var project = new Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                Status = projectDto.Status,
                Skills = projectDto.Skills,
                Interests = projectDto.Interests,
                StudentId = int.Parse(userId),
                FieldOfStudy = projectDto.FieldOfStudy,
                CreatedAt = System.DateTime.UtcNow,
                UpdatedAt = System.DateTime.UtcNow
            };
           
            var createdProject = await _projectService.CreateProjectAsync(project);

            var createdProjectDto = new ProjectDetailsDto
            {
                Id = createdProject.Id,
                Title = createdProject.Title,
                Description = createdProject.Description,
                Skills = createdProject.Skills,
                FieldOfStudy = createdProject.FieldOfStudy,
                Status = createdProject.Status,
                CreatedAt = createdProject.CreatedAt,
                UpdatedAt = createdProject.UpdatedAt,
                StudentId = int.Parse(userId)
            };

            return CreatedAtAction(nameof(GetProject), new { id = createdProjectDto.Id }, createdProjectDto);
        }

        // PUT: api/projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDto projectDto)
        {
            var project = new Project
            {
                Id = id,
                Title = projectDto.Title,
                Description = projectDto.Description,
                Status = projectDto.Status,
                UpdatedAt = System.DateTime.UtcNow
            };

            var updatedProject = await _projectService.UpdateProjectAsync(id, project);

            if (updatedProject == null)
            {
                return NotFound();
            }

            var updatedProjectDto = new ProjectDetailsDto
            {
                Id = updatedProject.Id,
                Title = updatedProject.Title,
                Description = updatedProject.Description,
                Status = updatedProject.Status,
                CreatedAt = updatedProject.CreatedAt,
                UpdatedAt = updatedProject.UpdatedAt,
                StudentId = updatedProject.StudentId
            };

            return Ok(updatedProjectDto);
        }

        // DELETE: api/projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var isDeleted = await _projectService.DeleteProjectAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet (nameof(GetRecommendedProjects))]
        public async Task<IActionResult> GetRecommendedProjects()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var projects= await _projectService.GetRecommendedProjects(int.Parse(userId));
            var projectDtos = projects.Select(p => new ProjectDetailsDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Skills = p.Skills,
                Interests = p.Interests,
                FieldOfStudy = p.FieldOfStudy,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                StudentId = p.StudentId
            }).ToList();

            return Ok(projectDtos);
        }
    }
}
