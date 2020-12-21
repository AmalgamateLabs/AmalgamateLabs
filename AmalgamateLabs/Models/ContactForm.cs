using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AmalgamateLabs.Models
{
    public class ContactForm
    {
        [Key]
        [Display(Name = "Contact form ID")]
        public int ContactFormId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "User e-mail address")]
        public string UserEmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required]
        [StringLength(7000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Message body")]
        public string MessageBody { get; set; }

        public async Task<bool> Submit(SystemConfig systemConfig, ILogger logger)
        {
            try
            {
                EmailAddress from = new EmailAddress(systemConfig.EmailAddress, "Amalgamate Labs Contact Form");
                EmailAddress to = new EmailAddress(systemConfig.EmailAddress, "Amalgamate Labs");
                string subject = $"SITE CONTACT FORM: {Subject}";
                string bodyText = $"Name: {Name}\nE-Mail: {UserEmailAddress}\n\n\n{MessageBody}";
                //string htmlContent = "<strong>and easy to do anywhere, even with C#</strong>"; //TODO: In the future, could make a fancy e-mail with HTML.
                SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, bodyText, htmlContent: null);

                Response response = await SendSendGridMessage(systemConfig, sendGridMessage);

                return true;
            }
            catch (Exception e)
            {
                logger.Error($"Failed to send contact form e-mail: {e.Message}");
                return false;
            }
        }

        private async Task<Response> SendSendGridMessage(SystemConfig systemConfig, SendGridMessage sendGridMessage)
        {
            DateTime dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (dateNow != systemConfig.SendGridLastActiveDateTime)
            {
                systemConfig.SendGridLastActiveDateTime = dateNow;
                systemConfig.SendGridEmailSendCount = 0;
            }

            if (systemConfig.CanUseSendGrid)
            {
                //string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY"); //TODO: Get this stuff out of source and use something like environment variables.
                SendGridClient sendGridClient = new SendGridClient(systemConfig.SendGridAPIKey);
                Response response = await sendGridClient.SendEmailAsync(sendGridMessage);
                systemConfig.SendGridEmailSendCount++;

                return response;
            }
            else
            {
                //TODO: Send using alternative method.
                throw new Exception("Met monthly SendGrid use.");
            }
        }
    }
}
