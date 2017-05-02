using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace win_eye.Plugins.DroneCI.BuildStatus
{
    public class BuildStatussesPluginCollector : IPluginCollector
    {
        private readonly EnvConfig m_EnvConfig;

        public BuildStatussesPluginCollector()
        {
            //TODO: Why is this needed for valid https/ssl website?
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            m_EnvConfig = EnvConfig.LoadFromEnvironment();
        }

        public IEnumerable<IPlugin> Collect()
        {
            return m_EnvConfig.DroneRepoPaths.Select(
                repoPath =>
                {
                    return new BuildStatusPlugin(m_EnvConfig.DroneURL, m_EnvConfig.DroneToken, repoPath);
                }).ToList();
        }
    }
}
