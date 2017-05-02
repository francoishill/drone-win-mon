using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace win_eye.Plugins.DroneCI.BuildStatus
{
    public class BuildStatusPlugin : IPlugin
    {
        public enum BuildStatus { Unknown, Success, Running }

        private readonly string m_DroneURL;
        private readonly string m_DroneToken;

        private readonly string m_RepoOwnerAndName;

        private PluginStatusEnum m_PluginStatus;

        public BuildStatusPlugin(string droneURL, string droneToken, string repoOwnerAndName)
        {
            m_DroneURL = droneURL;
            m_DroneToken = droneToken;
            m_RepoOwnerAndName = repoOwnerAndName;
        }

        public async Task Refresh()
        {
            var fullURL = m_DroneURL.TrimEnd('/') + $"/api/repos/{m_RepoOwnerAndName}/builds/latest";
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = $"Bearer {m_DroneToken}";

            var data = await webClient.DownloadDataTaskAsync(fullURL);
            var dataStr = Encoding.Default.GetString(data);

            var buildResponse = JsonConvert.DeserializeObject<BuildResponse>(dataStr);

            if (Enum.TryParse(buildResponse.Status, true, out BuildStatus tmpOutStatus))
            {
                switch (tmpOutStatus)
                {
                    case BuildStatus.Unknown:
                        m_PluginStatus = PluginStatusEnum.Unknown;
                        break;
                    case BuildStatus.Success:
                        m_PluginStatus = PluginStatusEnum.Success;
                        break;
                    case BuildStatus.Running:
                        m_PluginStatus = PluginStatusEnum.Problem;
                        break;
                    default:
                        m_PluginStatus = PluginStatusEnum.Unknown;
                        break;
                }
            }
            else
            {
                m_PluginStatus = PluginStatusEnum.Unknown;
            }
        }

        public PluginStatusEnum CurrentStatus()
        {
            return m_PluginStatus;
        }

        public string DisplayName()
        {
            return m_RepoOwnerAndName;
        }
    }
}
