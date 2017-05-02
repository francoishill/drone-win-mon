using System.Threading.Tasks;

namespace win_eye.Plugins
{
    public interface IPlugin
    {
        Task Refresh();
        PluginStatusEnum CurrentStatus();
        string DisplayName();
    }
}
