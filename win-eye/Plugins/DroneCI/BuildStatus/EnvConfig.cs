using System;
using System.Linq;

namespace win_eye.Plugins.DroneCI.BuildStatus
{
    public class EnvConfig
    {
        public string DroneURL;
        public string DroneToken;
        public string[] DroneRepoPaths;

        public EnvConfig(string droneURL, string droneToken, string droneRepoPaths)
        {
            DroneURL = droneURL;
            DroneToken = droneToken;
            DroneRepoPaths = string.IsNullOrWhiteSpace(droneRepoPaths) ? new string[0] : droneRepoPaths.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void Validate()
        {
            if (DroneRepoPaths == null || !DroneRepoPaths.Any())
            {
                throw new Exception("DroneRepoPaths must contain at least one element");
            }
        }

        public static EnvConfig LoadFromEnvironment()
        {
            var envFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "win-eye", ".env");
            var env = DotEnvFile.DotEnvFile.LoadFile(envFilePath, true);
            DotEnvFile.DotEnvFile.InjectIntoEnvironment(env);

            var cfg =  new EnvConfig(env["DRONE_URL"], env["DRONE_TOKEN"], env["DRONE_REPO_PATHS"]);
            cfg.Validate();
            return cfg;
        }
    }
}
