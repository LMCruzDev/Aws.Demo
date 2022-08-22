using System;
using System.Net;
using System.Net.Mail;
using Aws.Demo.Api.Configuration;
using Aws.Demo.Api.Controllers.Models.Emails;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Demo.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SesController : ControllerBase
    {
        private const int Port = 587;
        private readonly AwsConfiguration _awsConfiguration;

        public SesController(AwsConfiguration awsConfiguration)
        {
            _awsConfiguration = awsConfiguration;
        }

        [HttpPost("sendEmail")]
        public IActionResult SendEmail([FromBody] ApiSendEmailRequest emailRequest)
        {
            var mailMessage = new MailMessage
            {
                IsBodyHtml = !string.IsNullOrWhiteSpace(emailRequest.Body),
                From = new MailAddress(emailRequest.From, emailRequest.FromName),
                To = {emailRequest.To},
                Subject = emailRequest.Subject,
                Body = emailRequest.Body
            };

            using (var client = new SmtpClient(_awsConfiguration.SesSmtpHost, Port))
            {
                client.Credentials = new NetworkCredential(
                    _awsConfiguration.SesSmtpUsername,
                    _awsConfiguration.SesSmtpPassword);

                client.EnableSsl = true;

                try
                {
                    client.Send(mailMessage);
                }
                catch (Exception exception)
                {
                    return Problem(title: exception.Message, detail: exception.StackTrace);
                }
            }

            return Ok();
        }
    }
}