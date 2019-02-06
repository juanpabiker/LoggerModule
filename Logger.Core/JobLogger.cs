

namespace Logger.Core.Interface
{
    public class JobLogger
    {
        private IPlugin _plugin;
        
        public void setPlugin(IPlugin plugin)
        {
            _plugin = plugin;
        }

        public void log(string type, string message)
        {
            _plugin.Log(type, message);
        }

    }
}
