using System.Linq;
using System.Reflection;

namespace Logger.Core
{
    public class ModuleInfo
    {
        public string Name { get; set; }

        public Assembly Assembly { get; set; }
        
        public string Path { get; set; }

        public bool Enable { get; set; }

        public bool Exist { get; set; }
    }
}
