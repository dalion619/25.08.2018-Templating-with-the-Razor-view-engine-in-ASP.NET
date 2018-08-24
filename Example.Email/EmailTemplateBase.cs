using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Email.EmailViewModels;

namespace Example.Email
{

    /// <summary>
    /// Email Template Base View Model.
    /// </summary>
    public class EmailTemplateBase : IEmailTemplateBase
    {

        /// <summary>
        /// RazorEngine needs a parameterless constructor.
        /// </summary>
        public EmailTemplateBase()
        {
        }

        /// <summary>
        /// Creates the template base model, with all required values needed by the partial views.
        /// </summary>
        /// <param name="subject">Email subject ie. Order 12345.</param>
        /// <param name="preheader">Email preheader ie. Click to view your order.</param>
        /// <param name="googleGoToActionUrl">Destination url for Google Go-To Action button ie. yourstore.com/orders/vie.w12345.</param>
        /// <param name="favIconUrl">Url for your favicon.</param>
        /// <param name="copyHexColor">Color used for the content copy.</param>
        /// <param name="primaryHexColor">Color used for headings and CTA buttons.</param>
        /// <param name="headerUrl">Destination url when the header image is clicked on.</param>
        /// <param name="headerImageUrl">Url for your header image</param>
        /// <param name="socialNetworkList">List of social networks. <see cref="SocialNetworkViewModel"/></param>
        /// <param name="nameOfSender">Sender of the email ie. Your Store</param>
        /// <param name="footerUrl">Destination url when <see cref="NameOfSender"/> is clicked on.</param>
        public EmailTemplateBase(string subject, string preheader, string googleGoToActionUrl, string favIconUrl, string copyHexColor, string primaryHexColor, string headerUrl, string headerImageUrl, List<SocialNetworkViewModel> socialNetworkList, string nameOfSender, string footerUrl)
        {

            this.Subject = subject;
            this.Preheader = preheader;
            this.GoogleGoToActionUrl = googleGoToActionUrl;
            this.FavIconUrl = favIconUrl;
            this.CopyHexColor = copyHexColor;
            this.PrimaryHexColor = primaryHexColor;
            this.HeaderUrl = headerUrl;
            this.HeaderImageUrl = headerImageUrl;
            this.SocialNetworkList = socialNetworkList;
            this.NameOfSender = nameOfSender;
            this.FooterUrl = footerUrl;
        }
        public string Subject { get; private set; }
        public string Preheader { get; private set; }
        public string GoogleGoToActionUrl { get;  private set; }
        public string FavIconUrl { get; private set; }
        public string CopyHexColor { get; private set; }
        public string PrimaryHexColor { get;private set; }
        public string HeaderUrl { get;private set; }
        public string HeaderImageUrl { get; private set; }
        public List<SocialNetworkViewModel> SocialNetworkList { get; private set; }
        public string NameOfSender { get; private set; }
        public string FooterUrl { get; private set; }
    }
}
