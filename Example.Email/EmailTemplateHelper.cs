using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Example.Email.EmailViewModels;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Example.Email
{

    /// <summary>
    /// Helper for templating.
    /// </summary>
    public static class EmailTemplateHelper
    {

        public static async Task<List<SocialNetworkViewModel>> GetSocialNetworkListForFooter()
        {
            var temp = new List<SocialNetworkViewModel>()
            {
                new SocialNetworkViewModel("Twitter", "https://raw.githubusercontent.com/encharm/Font-Awesome-SVG-PNG/master/black/png/48/twitter-square.png", "https://twitter.com/da_lion_619"),
               new SocialNetworkViewModel("LinkedIn", "https://raw.githubusercontent.com/encharm/Font-Awesome-SVG-PNG/master/black/png/48/linkedin-square.png", "https://za.linkedin.com/in/lionelchetty"),
            };
            return temp.OrderBy(x => x.Name).ToList();
        }


        /// <summary>
        /// Renders HTML from email template view model passed in.
        /// </summary>
        /// <typeparam name="TEmailTemplate">The element type of the email template view model.</typeparam>
        /// <param name="model">Email template view model.</param>
        /// <returns>String of HTML.</returns>
        public static async Task<string> RenderEmailTemplate<TEmailTemplate>(TEmailTemplate model) where TEmailTemplate : IEmailTemplateBase
        {
            try
            {
                // Get RazorEngineService
                var razorEngineService = AppConfiguration.RazorEngineService;

                // Get email template type of generic model.
                var templateType = model.GetType();

                // Render HTML from view and model.
                var html = razorEngineService.RunCompile($"{templateType.Name}",
                    templateType, model);

                return html;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// Converts HTML email template to plain text.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static async Task<string> ConvertHtmlEmailTemplateToPlainText(string html)
        {
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(html);
                var plainTextStringBuilder = new StringBuilder();

                // Aka subject.
                var title = document.QuerySelector("title").TextContent;
                plainTextStringBuilder.AppendLine(title);
                plainTextStringBuilder.Append(Environment.NewLine);

                // Get all sections of content.
                var sections = document.All.Where(e => e.ClassList.Contains("section") && e.ClassList.Count() == 1);

                foreach (var section in sections)
                {
                    // Get heading of section.
                    var heading = section.GetElementsByClassName("h1").FirstOrDefault();
                    if (heading != null)
                    {
                        plainTextStringBuilder.Append(Environment.NewLine);
                        plainTextStringBuilder.AppendLine(heading.TextContent);
                        plainTextStringBuilder.Append(Environment.NewLine);
                    }

                    // Get paragraphs of content copy.
                    var paragraphs = section.GetElementsByClassName("copy");
                    if (paragraphs.Count() != 0)
                    {
                        foreach (var paragraph in paragraphs)
                        {
                            plainTextStringBuilder.AppendLine(paragraph.TextContent);
                        }
                        plainTextStringBuilder.Append(Environment.NewLine);
                    }

                    // Get CTA urls and titles.
                    var callToActions = section.GetElementsByTagName("a");
                    if (callToActions.Count() != 0)
                    {
                        foreach (var callToAction in callToActions)
                        {
                            plainTextStringBuilder.AppendLine($"{callToAction.TextContent} | {callToAction.GetAttribute("href")}");
                            plainTextStringBuilder.Append(Environment.NewLine);
                        }
                    }
                }

                // Get footer signature.
                var address = document.GetElementsByClassName("address").FirstOrDefault();
                if (address != null)
                {
                    /*
                     Text is malformed due to HTML formatting.
                     Split string by \n then disregard all elements that are empty after a Trim().
                     Select remaining elements Trim once more then join back into a string seperated by a Environment.NewLine
                     */
                    plainTextStringBuilder.AppendLine(string.Join(Environment.NewLine, (address.TextContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)).Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => s.Trim())));
                }
                return plainTextStringBuilder.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
