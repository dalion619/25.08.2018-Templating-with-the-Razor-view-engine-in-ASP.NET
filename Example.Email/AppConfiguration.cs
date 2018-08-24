using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Example.Email
{
    internal static class AppConfiguration
    {
        private static volatile bool _configLoaded = false;
        private static object _lock = new object();

        private static void EnsureConfigurationLoaded()
        {

            if (!_configLoaded)
            {
                lock (_lock)
                {
                    if (!_configLoaded)
                    {
                        NameValueCollection settings = null;

                        try
                        {
                            settings = ConfigurationManager.AppSettings;
                        }
                        finally
                        {
                            if (settings == null ||
                                string.IsNullOrWhiteSpace(_sendGridApiKey = settings["SendGridApiKey"]))
                            {
                                _sendGridApiKey = string.Empty;

                            }

                            if (_razorEngineService == null)
                            {
                                // Path needed for the ability to call this project in an ASP.NET project.
                                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                                var codeBaseUri = new UriBuilder(codeBase);
                                var uriPath = Uri.UnescapeDataString(codeBaseUri.Path);
                                var dirPath = Path.GetDirectoryName(uriPath);

                                // ResolvePathTemplateManager with no caching.
                                _razorEngineService = RazorEngine.Templating.RazorEngineService.Create(new TemplateServiceConfiguration
                                {
                                    TemplateManager = new ResolvePathTemplateManager(new[]
                                        {$@"{dirPath}\EmailPartialViews", $@"{dirPath}\EmailViews"}),
                                    DisableTempFileLocking = true,
                                    CachingProvider = new DefaultCachingProvider(t => { })
                                });
                            }
                            _configLoaded = true;
                        }

                    }
                }
            }
        }

        private static string _sendGridApiKey;
        internal static string SendGridApiKey
        {
            get
            {
                EnsureConfigurationLoaded();
                return _sendGridApiKey;
            }
        }

        private static IRazorEngineService _razorEngineService;
        internal static IRazorEngineService RazorEngineService
        {
            get
            {
                EnsureConfigurationLoaded();
                return _razorEngineService;

            }
        }
    }
}
