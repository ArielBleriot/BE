using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BridgeRTU.Interfaces;
using BridgeRTU.Services.Emails;

public class EmailService : IEmailService
{
    private readonly string _smtpHost = "mail.bridgertu.com"; // Your SMTP host
    private readonly int _smtpPort = 587; // Common port for TLS
    private readonly string _smtpUsername = "no-reply@bridgertu.com"; // Your SMTP email username
    private readonly string _smtpPassword = "H+OzXO{%zO)3"; // Your SMTP email password

    public async Task<bool> SendEmailAsync(EmailDto emailDTO)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailDTO.From??_smtpUsername),
            Subject = emailDTO.Subject,
            Body = emailDTO.Body,
            IsBodyHtml = true, // If the body is HTML, set it to true
        };

        // Add recipient(s)
        mailMessage.To.Add(emailDTO.To);

        var smtpClient = new SmtpClient(_smtpHost)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            UseDefaultCredentials=false,
            EnableSsl = true, // Enable SSL for secure connection
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage); // Send the email asynchronously
            return true;
        }
        catch (Exception ex)
        {
            return false;
            // Handle any errors (log, rethrow, etc.)
            throw new Exception($"Error sending email: {ex.Message}", ex);
        }
    }

}
