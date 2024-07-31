using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        // Check if emailSettings is null
        if (emailSettings == null)
        {
            throw new Exception("Email settings are not configured properly.");
        }

        var senderEmail = emailSettings["SenderEmail"];
        var senderPassword = emailSettings["SenderPassword"];
        var smtpServer = emailSettings["SmtpServer"];
        var portString = emailSettings["Port"];

        // Check for null or empty values
        if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword) ||
            string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(portString))
        {
            throw new Exception("Email settings are missing required information.");
        }

        // Safely parse the port value
        if (!int.TryParse(portString, out var port))
        {
            throw new Exception("Invalid port number in email settings.");
        }

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Rail's Mada", senderEmail));
        message.To.Add(new MailboxAddress("Recipient", to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();

        // Disable SSL certificate validation
        client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        try
        {
            await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            throw new Exception("Error sending email: " + e.Message);
        }
    }
}
