using BridgeRTU.Services.Activities.Dto;

namespace BridgeRTU.Interfaces
{
    public interface IActivityService
    {
        Task<ActivityDto> CreateAsync(CreateActivityDto activityDto);
        Task<ActivityDto> GetByIdAsync(int id);
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<IEnumerable<ActivityDto>> GetFilteredActivitiesAsync(ActivityFilterDto filterDto, int userdId);
        Task<IEnumerable<ActivityDto>> GetRecommendedActivitiesAsync(int userId);
        Task<ActivityDto> UpdateAsync(int id, UpdateActivityDto activityDto);
        Task<bool> DeleteAsync(int id);
    }

}
