using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Core
{
    public static class GlobalModules
    {
        static GlobalModules()
        {
            Modules = new List<ModuleInfo>();
        }

        public static IList<ModuleInfo> Modules { get; set; }
    }
}
