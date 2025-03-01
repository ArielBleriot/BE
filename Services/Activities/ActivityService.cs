using AutoMapper;
using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Persistance.Data;
using BridgeRTU.Services.Activities.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BridgeRTU.Services.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActivityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create activity
        public async Task<ActivityDto> CreateAsync(CreateActivityDto activityDto)
        {
            var activity = _mapper.Map<Activity>(activityDto);  // Map DTO to entity
            _context.Activity.Add(activity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ActivityDto>(activity);  // Map entity back to DTO
        }

        // Get activity by ID
        public async Task<ActivityDto> GetByIdAsync(int id)
        {
            var activity = await _context.Activity
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                throw new ValidationException("Activity not found.");

            return _mapper.Map<ActivityDto>(activity);
        }

        // Get all activities
        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            var activities = await _context.Activity
                .ToListAsync();

            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        // Get activities based on filter
        public async Task<IEnumerable<ActivityDto>> GetFilteredActivitiesAsync(ActivityFilterDto filterDto, int userId)
        {
            var student = await _context.Student.Where(x => x.Id == userId).FirstOrDefaultAsync();
            var query = _context.Activity.AsQueryable();

            if (!string.IsNullOrEmpty(filterDto.Location))
            {
                query = query.Where(a => a.Location.Contains(filterDto.Location));
            }
            if (filterDto.StartDate.HasValue)
            {
                query = query.Where(a => a.Date >= filterDto.StartDate.Value);
            }
            if (filterDto.BasedOnInterest == true)
            {
                var data= query.AsEnumerable().Where(a => a.Interest.Any(i => student.PersonalInterests.Contains(i))).ToList();
                var result=_mapper.Map<IEnumerable<ActivityDto>>(data);
                return result;
            }




            var activities = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        // Update activity
        public async Task<ActivityDto> UpdateAsync(int id, UpdateActivityDto activityDto)
        {
            var activity = await _context.Activity
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                throw new ValidationException("Activity not found.");

            // Update the basic fields
            activity.Name = activityDto.Name;
            activity.Description = activityDto.Description;
            activity.Date = activityDto.Date;
            activity.Location = activityDto.Location;


            await _context.SaveChangesAsync();
            return _mapper.Map<ActivityDto>(activity);
        }

        // Delete activity
        public async Task<bool> DeleteAsync(int id)
        {
            var activity = await _context.Activity
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                return false;

            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ActivityDto>> GetRecommendedActivitiesAsync(int userId)
        {
            var student = await _context.Student.AsNoTracking().Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
            var query = _context.Activity.AsQueryable();

            if (student is not null)
            {
                var activities = query.AsEnumerable()
    .Where(a => student.PersonalInterests.Any(interest => a.Interest.Contains(interest)));

                return _mapper.Map<IEnumerable<ActivityDto>>(activities);
            }
            return Enumerable.Empty<ActivityDto>();
        }
    }

}
