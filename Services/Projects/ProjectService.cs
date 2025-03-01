using AutoMapper;
using AutoMapper.QueryableExtensions;
using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Persistance.Data;
using BridgeRTU.Services.Projects.Dto;
using Microsoft.EntityFrameworkCore;

namespace BridgeRTU.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProjectService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<Project> CreateProjectAsync(Project request)
        {
            var student = await _context.Student.Where(x => x.Id == request.StudentId).FirstOrDefaultAsync();
            request.FieldOfStudy = student!.FieldOfStudy;
            var project = _mapper.Map<Project>(request);
            await _context.Project.AddAsync(project);
            var result=await _context.SaveChangesAsync();
            return project;
        }

        public Task<bool> DeleteProjectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectDetailsDto>> GetAllProjectsAsync()
        {
            return await _context.Project.AsNoTracking().Include(x=>x.Comments).Include(x=>x.Student).ProjectTo<ProjectDetailsDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
       
        public async Task<List<Project>> GetRecommendedProjects(int userId)
        {
            var student = await _context.Student.AsNoTracking().Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
            var query = _context.Project.AsQueryable();

            if (student is not null)
            {
                query = query.Where(project => student.FieldOfStudy.Contains(project.FieldOfStudy));
            }
            return await query.ToListAsync();
        }

        public Task<Project> GetProjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateProjectAsync(int id, Project project)
        {
            throw new NotImplementedException();
        }
    }
}
