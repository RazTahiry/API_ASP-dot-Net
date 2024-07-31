using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class emailController(EmailService emailService) : ControllerBase
    {
        private readonly EmailService _emailService = emailService;

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            try
            {
                await _emailService.SendEmailAsync(to, subject, body);
                return Ok("Email sent successfully!");
            }
            catch (Exception e)
            {
                return BadRequest($"Error sending email: {e.Message}");
            }
        }
    }
}