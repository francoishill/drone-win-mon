using System;
using System.Diagnostics;
using System.Threading.Tasks;
using win_eye.Plugins;

namespace win_eye.Plugins
{
    public class PluginViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private IPlugin m_Plugin;

        public PluginViewModel(IPlugin plugin)
        {
            m_Plugin = plugin;

            ContinuallyRefresh();
        }

        private async void ContinuallyRefresh()
        {
            while (true)
            {
                var stopWatch = Stopwatch.StartNew();

                await m_Plugin.Refresh();

                stopWatch.Stop();
                if (stopWatch.Elapsed < TimeSpan.FromSeconds(3))
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

                Status = m_Plugin.CurrentStatus().ToString();
                HasProblem = m_Plugin.CurrentStatus() != PluginStatusEnum.Success;
                IsSuccessful = m_Plugin.CurrentStatus() == PluginStatusEnum.Success;
            }
        }

        public string DisplayName => m_Plugin.DisplayName();

        private string m_Status = PluginStatusEnum.Unknown.ToString();
        public string Status
        {
            get
            {
                return m_Status;
            }
            private set
            {
                if (m_Status == value) return;
                m_Status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        private bool m_HasProblem;
        public bool HasProblem
        {
            get
            {
                return m_HasProblem;
            }
            private set
            {
                if (m_HasProblem == value) return;
                m_HasProblem = value;
                RaisePropertyChanged(nameof(HasProblem));
            }
        }

        private bool m_IsSuccessful;
        public bool IsSuccessful
        {
            get
            {
                return m_IsSuccessful;
            }
            private set
            {
                if (m_IsSuccessful == value) return;
                m_IsSuccessful = value;
                RaisePropertyChanged(nameof(IsSuccessful));
            }
        }
    }
}
