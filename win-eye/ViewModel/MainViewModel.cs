using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using win_eye.Plugins;
using win_eye.Plugins.DroneCI.BuildStatus;

namespace win_eye.ViewModel
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
        private readonly List<IPluginCollector> m_PluginCollectors = new List<IPluginCollector>()
        {
            new BuildStatussesPluginCollector(),
        };

        private readonly ObservableCollection<PluginViewModel> m_PluginViewModels;
        public ICollection<PluginViewModel> PluginViewModels => m_PluginViewModels;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var pluginViewModels = GetPlugins()
                .Select(p => new PluginViewModel(p));
            m_PluginViewModels = new ObservableCollection<PluginViewModel>(pluginViewModels);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private ICollection<IPlugin> GetPlugins()
        {
            var list = new List<IPlugin>();
            foreach (var collector in m_PluginCollectors)
            {
                list.AddRange(collector.Collect());
            }

            return list;
        }
    }
}