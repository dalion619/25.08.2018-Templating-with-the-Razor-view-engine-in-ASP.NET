using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Email.EmailViewModels
{

    /// <summary>
    ///  Social Network Information View Model.
    /// </summary>
    public class SocialNetworkViewModel
    {

        /// <summary>
        /// Creates a model containing your social network information.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iconUrl"></param>
        /// <param name="socialUrl"></param>
        public SocialNetworkViewModel(string name, string iconUrl, string socialUrl)
        {
            this.Name = name;
            this.IconUrl = iconUrl;
            this.SocialUrl = socialUrl;
        }
        public string Name { get; private set; }
        public string IconUrl { get; private set; }
        public string SocialUrl { get; private set; }

    }
}
