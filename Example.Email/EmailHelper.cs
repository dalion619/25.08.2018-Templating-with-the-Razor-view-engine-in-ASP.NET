using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace Example.Email
{
    /// <summary>
    /// Helper for sending emails.
    /// </summary>
    public static class EmailHelper
    {
        
        /// <summary>
        /// Sends an email message through SendGrid.
        /// </summary>
        /// <param name="replyToAddress">Reply address ie. no-reply@yourstore.com.</param>
        /// <param name="fromAddress">From address ie. communication@yourstore.com.</param>
        /// <param name="recipientAddresses">List of addresses to send emails to.</param>
        /// <param name="subjectLine">Subject of the email message.</param>
        /// <param name="plainTextContent">Plain text version of the email content.</param>
        /// <param name="htmlContent">HTML content version of the email content.</param>
        /// <param name="categories">List of category strings for SendGrid analytics.</param>
        /// <returns></returns>
        public static async Task<bool> SendEmailMessage(EmailAddress replyToAddress, EmailAddress fromAddress, List<EmailAddress> recipientAddresses, string subjectLine, string plainTextContent, string htmlContent, List<string> categories)
        {
            try
            {
                var singleEmail =
                    MailHelper.CreateSingleEmailToMultipleRecipients(fromAddress, recipientAddresses, subjectLine, plainTextContent, htmlContent);
                singleEmail.SetReplyTo(replyToAddress);
                singleEmail.AddCategories(categories);

                var sendGridClient = new SendGrid.SendGridClient(AppConfiguration.SendGridApiKey);
                var sendEmail = await sendGridClient.SendEmailAsync(singleEmail);
                return sendEmail.StatusCode != System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
