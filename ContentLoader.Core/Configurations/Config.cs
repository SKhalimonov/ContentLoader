using System.Collections.Generic;

namespace ContentLoader.Core.Configurations
{
    public class Config
    {
        public Dictionary<string, ServiceConfig> ServicesConfigs { get; set; }

        public string[] AllowedHosts { get; set; }
    }
}
