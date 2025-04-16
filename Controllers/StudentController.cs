// Controllers/UserController.cs
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using BridgeRTU.Interfaces;
using BridgeRTU.Services.Students;
using BridgeRTU.Services.Students.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IEmailService _emailService;

    public StudentController(IStudentService studentService, IEmailService emailService)
    {
        _studentService = studentService;
        _emailService = emailService;
    }

    // POST: api/user/signup
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] StudentSignupDto studentSignupDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _studentService.CreateUserAsync(studentSignupDto);
            return Ok(new { message = "Signup successful", userId = user.Id });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var data = await _studentService.LoginAsync(model.Email, model.Password);
            return Ok(new { StudentId = data.Item1, Token = data.Item2, FullName = data.Item3, Email = data.Item4, UniversityName = data.Item5 });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
    [HttpGet("Profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
       var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _studentService.GetProfile(int.Parse(userId)));
    }
    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] PasswordResetRequestDto request)
    {
        var result = await _studentService.GeneratePasswordResetLinkAsync(request.Email);

        return Ok(result);
    }
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ConfigurePasswordRequestDto request)
    {
        var result = await _studentService.ConfigurePassword(request);

        return Ok(result);
    }
    [HttpPost("ActivityRegistration")]
    public async Task<IActionResult> RegisterForActivity([FromBody] ActivityRegistrationRequestDto request)
    {
        var result = await _studentService.RegisterForActivity(request);
        return Ok(new{ Successfull=result.Item1,Message=result.Item2});
    }
    [HttpPost("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<StudentSearchResultDto>>> SearchStudents([FromBody] StudentSearchDto searchDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            searchDto.CurrentStudentId = int.Parse(userId);
            var results = await _studentService.SearchStudentsAsync(searchDto);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while searching for students.");
        }
    }
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] StudentProfileUpdateDto profileDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedProfile = await _studentService.UpdateProfileAsync(userId, profileDto);
            return Ok(updatedProfile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}
