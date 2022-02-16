namespace BugTracker.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Services.Messaging;

    public class EmailService : IEmailService
    {
        private readonly IEmailSender emailSender;

        public EmailService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task SendWelcomeEmailAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            await this.emailSender.SendEmailAsync(from, fromName, to, subject, "<h1>Welcome To BugTracker!<h1>");
        }

        public async Task SendSubscribeEmailAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            await this.emailSender.SendEmailAsync(from, fromName, to, subject, "<h4>Successfully <strong>SUBSCRIBE</strong> to our news!<h4>");
        }

        public async Task SendUnsubscribeEmailAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            await this.emailSender.SendEmailAsync(from, fromName, to, subject, "<h4>Successfully <strong>Unsubscribe</strong> to our news!<h4>");
        }

        public Task SendEmailForNewMemberAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailForNewRegisterMemberAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailForResendEmailConfirmationAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailForForgotPasswordAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            throw new NotImplementedException();
        }
    }
}
