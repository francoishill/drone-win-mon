using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace win_eye.Drone
{
    public class BuildWatcher
    {
        public enum BuildStatus { Unknown, Success, Running }

        private readonly string m_DroneURL;
        private readonly string m_DroneToken;

        private readonly string m_RepoOwnerAndName;
        public string RepoOwnerAndName => m_RepoOwnerAndName;

        private BuildStatus m_CurrentStatus;
        public BuildStatus CurrentStatus => m_CurrentStatus;

        public BuildWatcher(string droneURL, string droneToken, string repoOwnerAndName)
        {
            m_DroneURL = droneURL;
            m_DroneToken = droneToken;
            m_RepoOwnerAndName = repoOwnerAndName;
        }

        public async Task DoRefresh()
        {
            var fullURL = m_DroneURL.TrimEnd('/') + $"/api/repos/{m_RepoOwnerAndName}/builds/latest";
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = $"Bearer {m_DroneToken}";
                        
            var data = await webClient.DownloadDataTaskAsync(fullURL);
            var dataStr = Encoding.Default.GetString(data);            

            var buildResponse = JsonConvert.DeserializeObject<BuildResponse>(dataStr);

            BuildStatus tmpOutStatus;
            if (Enum.TryParse(buildResponse.Status, true, out tmpOutStatus))
            {
                m_CurrentStatus = tmpOutStatus;
            }
            else
            {
                m_CurrentStatus = BuildStatus.Unknown;
            }
        }
    }
}
