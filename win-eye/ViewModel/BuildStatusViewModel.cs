using win_eye.Drone;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace win_eye.ViewModel
{
    public class BuildStatusViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private BuildWatcher m_BuildWatcher;

        public BuildStatusViewModel(BuildWatcher buildWatcher)
        {
            m_BuildWatcher = buildWatcher;

            ContinuallyRefresh();
        }

        private async void ContinuallyRefresh()
        {
            while (true)
            {
                var stopWatch = Stopwatch.StartNew();

                await m_BuildWatcher.DoRefresh();

                stopWatch.Stop();
                if (stopWatch.Elapsed < TimeSpan.FromSeconds(3))
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

                Status = m_BuildWatcher.CurrentStatus.ToString();
                HasProblem = m_BuildWatcher.CurrentStatus != BuildWatcher.BuildStatus.Success;
                IsSuccessful = m_BuildWatcher.CurrentStatus == BuildWatcher.BuildStatus.Success;
            }
        }

        public string RepoAndOwner => m_BuildWatcher.RepoOwnerAndName;

        private string m_Status = BuildWatcher.BuildStatus.Unknown.ToString();
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
