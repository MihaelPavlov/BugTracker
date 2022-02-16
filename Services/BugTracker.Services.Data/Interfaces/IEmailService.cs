namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendSubscribeEmailAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendUnsubscribeEmailAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendEmailForNewMemberAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendEmailForNewRegisterMemberAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendEmailForResendEmailConfirmationAsync(string from, string fromName, string to, string subject, string htmlContent);

        Task SendEmailForForgotPasswordAsync(string from, string fromName, string to, string subject, string htmlContent);
    }
}
