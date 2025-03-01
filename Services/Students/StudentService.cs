using AutoMapper;
using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Persistance.Data;
using BridgeRTU.Services.Students.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BridgeRTU.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public StudentService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration, IEmailService emailService, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
            _tokenService = tokenService;
        }
        public async Task<ConfigurePasswordResponseDto> ConfigurePassword(ConfigurePasswordRequestDto request)
        {
            // Validate the token
            var tokenInfo = await _tokenService.GetPasswordResetToken(request.Token);
            if (tokenInfo.IsUsed)
            {
                return new ConfigurePasswordResponseDto { Successfull=false,Message="Link expired"}; // Invalid or expired token
            }

            // Find the user by token information (Assuming the token has userId info)
            var user = await _context.Student.FirstOrDefaultAsync(x => x.Email == tokenInfo.UserEmail);
            if (user == null)
            {
                return new ConfigurePasswordResponseDto { Successfull = false, Message = "User not found" }; // Invalid or expired token
            }

            // Reset the password for the user
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword); // Assuming you have a password hashing utility
            user.Password = passwordHash;

            // Mark the token as used (e.g., delete or deactivate the token in DB)
            await _tokenService.MarkTokenAsUsedAsync(request.Token);

            // Save the updated user data to the database
            _context.Student.Update(user);
            await _context.SaveChangesAsync();
            return new ConfigurePasswordResponseDto { Successfull = true, Message = "Password reset success" }; // Invalid or expired token;
        }
        public async Task<bool> GeneratePasswordResetLinkAsync(string email)
        {
            // Check if the user exists
            var user = await _context.Student.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false; // User not found
            }

            // Generate a reset token
            var resetToken = await _tokenService.GeneratePasswordResetTokenAsync(user.Email);

            // Create the reset URL (this could be a URL pointing to your front-end)
            var resetUrl = $"{_configuration["PasswordResetLink"]}token={resetToken}";

            // Send email to the user with the reset link
            var emailSent = await _emailService.SendEmailAsync(new Emails.EmailDto { To=user.Email,Body=$"Follow below link to reset your password\n{resetUrl} Link will be expired after 1 hour",Subject="Password Reset"});

            return emailSent;
        }
        public async Task<Student> CreateUserAsync(StudentSignupDto userDto)
        {
            // Password validation
            if (userDto.Password != userDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }
            var userExists = await _context.Student.Where(x => x.Email.Equals(userDto.Email)).FirstOrDefaultAsync();
            if (userExists is not null)
            {
                throw new ArgumentException("Email exists.");
            }

            // Hash the password (you should use a real hashing method here like bcrypt or PBKDF2)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            // Create the User entity from DTO
            var user = new Student
            {
                Email = userDto.Email,
                FullName= userDto.FullName,
                UniversityName = userDto.UniversityName,
                FieldOfStudy = userDto.FieldOfStudy,
                PersonalInterests = userDto.PersonalInterests,
                Gender = userDto.Gender,
                Password = hashedPassword,
                RememberMe = userDto.RememberMe
            };

            // Save the user to the database
            _context.Student.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Student> GetProfile(int userId)
        {
            return await _context.Student.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<Tuple<int,string,string>> LoginAsync(string email, string password)
        {
            // Step 1: Find the student by email
            var student = await _context.Student.SingleOrDefaultAsync(s => s.Email == email);

            if (student == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            // Step 2: Compare the provided password with the stored hashed password
            if (!BCrypt.Net.BCrypt.Verify(password, student.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            // Step 3: Generate JWT token if login is successful
            var token = GenerateJwtToken(student);

            return new Tuple<int,string, string> (student.Id,token,student.FullName);
        }

        private string GenerateJwtToken(Student student)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
            new Claim(ClaimTypes.Name, student.Email),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,  // Add issuer if necessary
                audience: null,  // Add audience if necessary
                claims: claims,
                expires: DateTime.Now.AddHours(1),  // Set the token expiration
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Tuple<bool,string>> RegisterForActivity(ActivityRegistrationRequestDto registration)
        {
            var student = await _context.Student.SingleOrDefaultAsync(s => s.Id == registration.StudentId);

            if (student == null)
                return new Tuple<bool, string>(false, "Student not exists");
            var activity = await _context.Activity.SingleOrDefaultAsync(s => s.Id == registration.ActivityId);

            if (activity == null)
                return new Tuple<bool, string>(false, "Activity not exists");
            var studentRegisteredAlready = await _context.ActivityRegistration.Include(x => x.Student).Include(x => x.Activity)
                                           .FirstOrDefaultAsync(x => x.Student.Id == registration.StudentId && x.Activity.Id == registration.ActivityId);
            if (studentRegisteredAlready is not null)
                return new Tuple<bool, string>(false, "You had already registered for this activity");
            if (activity.SeatsLeft<=0)
            {
                activity.Status = "Sold Out";
                return new Tuple<bool, string>(false, "No more seat's left");
            }
            var activityRegistration = new ActivityRegistration
            {
                Student = student,
                Activity = activity,
                RegistrationDate = DateTime.UtcNow
            };
            activity.SeatsLeft -= 1;
            _context.ActivityRegistration.Add(activityRegistration);
            _context.Update(activity);
            await _context.SaveChangesAsync();
            var emailBody = GenerateEmailBody(activity, student.FullName);
            var result=await _emailService.SendEmailAsync(new Emails.EmailDto { Body = emailBody,Subject="BridgeRTU Activity Registration",To=student.Email});
            return new Tuple<bool, string>(true, "You response is recorded");
        }
        public string GenerateEmailBody(Activity activity, string userName)
        {
            return $@"
        <html>
            <body>
                <h2>Hello {userName},</h2>
                <p>Thank you for registering for the activity: <strong>{activity.Name}</strong>.</p>
                <p>Here are the details of the activity:</p>
                <ul>
                    <li><strong>Description:</strong> {activity.Description}</li>
                    <li><strong>Date:</strong> {activity.Date:MMMM dd, yyyy}</li>
                    <li><strong>Location:</strong> {activity.Location}</li>
                </ul>
                <p>We hope you enjoy the activity!</p>
                <p>If you have any questions, feel free to reach out to us at support@bridgertu.com</p>
                <p>Best regards,<br/>The Activity Team</p>
            </body>
        </html>";
        }

    }
}
