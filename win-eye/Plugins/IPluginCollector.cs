using System.Collections.Generic;

namespace win_eye.Plugins
{
    public interface IPluginCollector
    {
        IEnumerable<IPlugin> Collect();
    }
}
