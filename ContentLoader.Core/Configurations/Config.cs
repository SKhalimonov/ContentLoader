using System.Collections.Generic;
using System.IO;

namespace ContentLoader.Core.Configurations
{
    public class Config
    {
        public Dictionary<string, ServiceConfig> ServicesConfigs { get; set; }

        public string[] AllowedHosts { get; set; }

        public string ContentRootPath { get; set; }

        public string BrowserEngineName { get; set; }

        public string GetBrowserEnginePath()
        {
            return Path.Combine(ContentRootPath, BrowserEngineName);
        }
    }
}
