using System.Security.Claims;
using BridgeRTU.Interfaces;
using BridgeRTU.Services.Activities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BridgeRTU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto activityDto)
        {
            var activity = await _activityService.CreateAsync(activityDto);
            return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
        }
        [HttpPost(nameof(GetFilteredActivities))]
        public async Task<IActionResult> GetFilteredActivities([FromBody] ActivityFilterDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var activities = await _activityService.GetFilteredActivitiesAsync(request,int.Parse(userId));
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _activityService.GetByIdAsync(id);
            return Ok(activity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            var activities = await _activityService.GetAllAsync();
            return Ok(activities);
        }
        [HttpGet(nameof(GetRecommendedActivities))]
        public async Task<IActionResult> GetRecommendedActivities()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var activities = await _activityService.GetRecommendedActivitiesAsync(userId);
            return Ok(activities);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDto activityDto)
        {
            var updatedActivity = await _activityService.UpdateAsync(id, activityDto);
            return Ok(updatedActivity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var deleted = await _activityService.DeleteAsync(id);
            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}
