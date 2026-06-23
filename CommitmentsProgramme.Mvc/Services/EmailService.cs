using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CommitmentsProgramme.Mvc.Services;

public class EmailService(IOptions<EmailSettings> options) : IEmailService
{
	private readonly EmailSettings _options = options.Value;

    public async Task SendEmailAsync(string toEmail, string subject, string body)
	{
		var message = new MailMessage(_options.From, toEmail, subject, body);
		message.IsBodyHtml = true;

		using var client = new SmtpClient(_options.SmtpServer, _options.Port)
		{
			Credentials = new NetworkCredential(_options.Username, _options.Password),
			EnableSsl = true
		};

		await client.SendMailAsync(message);
	}
}
