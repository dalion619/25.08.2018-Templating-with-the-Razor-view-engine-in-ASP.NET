using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Example.Email.EmailViewModels;
using SendGrid.Helpers.Mail;

namespace Example.Email
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var firstName = "LioneL";
            var middleName = "Christopher";
            var lastName = "Chetty";

            var nameOfSender = $"{firstName} {middleName} {lastName}";

            var emailSubject = $"Bits & Bytes | Welcome {firstName} {lastName} ❤️";
            var emailPreheader = $"Click to view my latest posts 💜";
            var emailFavIconUrl = "https://lionelchetty.co.za/favicon.ico";
            var emailHeaderImageUrl = "https://lionelchetty.co.za/Resources/images/bitsnbytes.png";
            var emailFooterUrl = "https://lionelchetty.co.za";

            var emailCopyColor = "#888888";
            var emailPrimaryColor = "#7f00ff";
            var emailCallToActionUrl = "https://blog.lionelchetty.co.za";
            var emailFooterSocialNetworks = await EmailTemplateHelper.GetSocialNetworkListForFooter();

          
            var welcomeEmailTemplateModel = new WelcomeEmailTemplate(firstName, lastName, emailCallToActionUrl, emailSubject, emailPreheader,
                emailFavIconUrl, emailCopyColor, emailPrimaryColor, emailCallToActionUrl, emailHeaderImageUrl, emailFooterSocialNetworks, $"{nameOfSender}", emailFooterUrl);

            var emailHtmlBody = await EmailTemplateHelper.RenderEmailTemplate<WelcomeEmailTemplate>(welcomeEmailTemplateModel);
            var emailPlainTextBody = await EmailTemplateHelper.ConvertHtmlEmailTemplateToPlainText(emailHtmlBody);
            var result = await EmailHelper.SendEmailMessage(
                new EmailAddress("dev@lionelchetty.co.za",   nameOfSender),
                new EmailAddress("mail@lionelchetty.co.za ", nameOfSender),
                new List<EmailAddress>() { new EmailAddress("dev@lionelchetty.co.za", $"{firstName} {lastName}")},
                emailSubject, emailPlainTextBody, emailHtmlBody, new List<string>() { "Welcome" });
            
        }
    }
}
