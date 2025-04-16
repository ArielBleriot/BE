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
        private readonly IStudentService _studentService;

        public EmailController(IEmailService emailService, IStudentService studentService)
        {
            _emailService = emailService;
            _studentService = studentService;
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

        [HttpPost("send-verification")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            var result = await _studentService.SendEmailVerificationCodeAsync(email);
            if (result)
                return Ok(new { message = "Verification code sent successfully" });
            return BadRequest(new { message = "Failed to send verification code" });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            var result = await _studentService.VerifyEmailAsync(request.Email, request.Code);
            if (result)
                return Ok(new { message = "Email verified successfully" });
            return BadRequest(new { message = "Invalid or expired verification code" });
        }
    }

    public class VerifyEmailRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
