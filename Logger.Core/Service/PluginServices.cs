using Logger.Core.Interface;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Logger.Core
{
 
    public class PluginServices
    {
        private static string _conexion;

        public PluginServices(string conexion)
        {
            _conexion = conexion;
        }

        public void ExcecuteLog(string type, string messege)
        {
            LoadPlugins();
            var job = new JobLogger();
            foreach (var module in GlobalModules.Modules)
            {
                if(!module.Enable || !module.Exist)
                {
                    continue;
                }
                               
                var plugin = GetIPlugin(module.Assembly);
                job.setPlugin(plugin);
                job.log(type, messege);
            }
        }

        private static IPlugin GetIPlugin(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (!type.IsClass || (type.GetInterface(typeof(IPlugin).FullName) == null))
                {
                    continue;                    
                }

                ConstructorInfo ctor = type.GetConstructors().FirstOrDefault();
                if (ctor.GetParameters().Length == 0)
                {
                    return (ctor.Invoke(new object[] { }) as IPlugin);
                }

                return (ctor.Invoke(new object[] { _conexion }) as IPlugin);
            }

            return null;
        }

        public void LoadPlugins()
        {
            var pluginsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Modules");
            var ddls = Directory.GetFiles(pluginsFolder, "Logger*.dll", SearchOption.TopDirectoryOnly);

            foreach (var module in GlobalModules.Modules)
            {
                if(!module.Enable)
                {
                    continue;
                }

                if(module.Path == null)
                {
                    if(!ddls.Any(p => p.Contains(module.Name)))
                    {
                        module.Exist = false;
                        continue;
                    }

                    module.Exist = true;
                    module.Path = ddls.FirstOrDefault(p => p.Contains(module.Name));
                }
                else
                {
                    if (!File.Exists(module.Path))
                    {
                        module.Exist = false;
                        continue;
                    }
                }                
                                
                var assembly = AssemblyLoader.LoadFromAssemblyPath(module.Path);                                
                var types = assembly.GetExportedTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && (type.GetInterface(typeof(IPlugin).FullName) != null) )
                    {
                        module.Assembly = assembly;
                        module.Exist = true;
                        break;
                    }

                    module.Exist = false;
                }
            }
        }
    }
}
