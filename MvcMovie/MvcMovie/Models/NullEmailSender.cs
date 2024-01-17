using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

public class NullEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Do nothing, as we're not sending actual emails
        return Task.CompletedTask;
    }
}