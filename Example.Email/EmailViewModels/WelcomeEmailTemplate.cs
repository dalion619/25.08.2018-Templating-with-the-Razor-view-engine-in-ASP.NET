using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Email;

namespace Example.Email.EmailViewModels
{

    /// <summary>
    /// Welcome Email Template View Model.
    /// </summary>
    public class WelcomeEmailTemplate:EmailTemplateBase
    {
        /// <summary>
        /// RazorEngine needs a parameterless constructor.
        /// </summary>
        public WelcomeEmailTemplate()
        {

        }


        /// <inheritdoc />
        /// 
        public WelcomeEmailTemplate(string firstName, string lastName, string callToActionUrl,string subject, string preheader, string favIconUrl,
            string copyHexColor, string primaryHexColor, string headerUrl, string headerImageUrl, List<SocialNetworkViewModel> socialNetworkList,string nameOfSender, string footerUrl) : base(subject,
            preheader,callToActionUrl, favIconUrl, copyHexColor, primaryHexColor,headerUrl,  headerImageUrl, socialNetworkList, nameOfSender,footerUrl)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CallToActionUrl = callToActionUrl;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CallToActionUrl { get; private set; }


    }
}
