using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace BridgeRTU.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context; // A repository to interact with the database
        private readonly IConfiguration _configuration; // To access token expiration settings

        public TokenService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Generate password reset token and save it to the database
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {

            // Generate a secure token (could use GUID or JWT here, for simplicity let's use GUID)
            var token = Guid.NewGuid().ToString();

            // Set the expiration time (e.g., 1 hour)
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Create the reset token entity
            var resetToken = new PasswordResetToken
            {
                Token = token,
                UserEmail = email,
                ExpiryDate = expiryDate,
                IsUsed = false
            };

            // Save token to the database
            _context.PasswordResetToken.Add(resetToken);
            await _context.SaveChangesAsync();

            return token;
        }

        // Validate the password reset token
        public async Task<PasswordResetToken> GetPasswordResetToken(string token)
        {
            // Get the token from the database
            var resetToken = await _context.PasswordResetToken.FirstOrDefaultAsync(x=>x.Token==token);
            if (resetToken == null || resetToken.IsUsed || resetToken.ExpiryDate < DateTime.UtcNow)
            {
                return resetToken; // Token is either invalid, expired, or already used
            }

            return resetToken;
        }

        // Mark the token as used when the user resets their password
        public async Task MarkTokenAsUsedAsync(string token)
        {
            // Retrieve the token from the database
            var resetToken = await _context.PasswordResetToken.FirstOrDefaultAsync(x => x.Token == token);
            if (resetToken == null)
            {
                throw new ArgumentException("Invalid token");
            }

            resetToken.IsUsed = true;

            // Save the updated token status to the database
            _context.PasswordResetToken.Update(resetToken);
            await _context.SaveChangesAsync();
        }
    }

}
