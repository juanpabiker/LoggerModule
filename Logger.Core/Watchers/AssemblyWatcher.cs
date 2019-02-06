using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Logger.Core.Watchers
{
    public class AssemblyWatcher
    {
        private FileSystemWatcher fileSystemWatcher;
        private string folderToWatchFor = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Modules");
        private static PluginServices _pluginServices;

        public AssemblyWatcher(PluginServices pluginServices)
        {
            _pluginServices = pluginServices;

            fileSystemWatcher = new FileSystemWatcher(folderToWatchFor);
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Filter = "Logger*.dll";
            fileSystemWatcher.Deleted += new FileSystemEventHandler(CallLoadPlugin);
            fileSystemWatcher.Created += new FileSystemEventHandler(CallLoadPlugin);
            fileSystemWatcher.Changed += new FileSystemEventHandler(CallLoadPlugin);
        }

        private void CallLoadPlugin(Object sender, FileSystemEventArgs e)
        {
            _pluginServices.LoadPlugins();
        }
    }
}
