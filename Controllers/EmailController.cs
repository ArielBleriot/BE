using BridgeRTU.Interfaces;
using BridgeRTU.Services.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BridgeRTU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            try
            {
                var result = await _emailService.SendEmailAsync(emailDto);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Email sent successfully" });
                }
                return StatusCode(500, new { Success = false, Message = "Failed to send email" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Failed to send email", Error = ex.Message });
            }
        }
    }
}
