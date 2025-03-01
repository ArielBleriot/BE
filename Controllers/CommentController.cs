using System.Security.Claims;
using BridgeRTU.Interfaces;
using BridgeRTU.Services.Activities.Dto;
using BridgeRTU.Services.Comments.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BridgeRTU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            var comment = await _commentService.CreateComment(commentDto);
            return Ok(comment);
        }

    }

}
