using drone_win_mon.Drone;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace drone_win_mon.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly EnvConfig m_EnvConfig;

        private readonly ObservableCollection<BuildStatusViewModel> m_BuildStatusViewModels;
        public ICollection<BuildStatusViewModel> BuildStatusViewModels => m_BuildStatusViewModels;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //TODO: Why is this needed for valid https/ssl website?
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            m_EnvConfig = EnvConfig.LoadFromEnvironment();

            m_BuildStatusViewModels = new ObservableCollection<BuildStatusViewModel>(
                m_EnvConfig.DroneRepoPaths.Select(
                repoPath => {
                    return new BuildStatusViewModel(new BuildWatcher(m_EnvConfig.DroneURL, m_EnvConfig.DroneToken, repoPath));
                }).ToList());

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}