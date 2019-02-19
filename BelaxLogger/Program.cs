using Logger.Core;
using Logger.Core.Watchers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BelaxLogger
{
    internal class Program
    {
        private static PluginServices _pluginServices;

        private static void Main(string[] args)
        {
            Inicializar();
            Consola();
        }

        private static void Inicializar()
        {
            Console.WriteLine("--- Starting Logger Console App ---");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            _pluginServices = new PluginServices(configuration.GetConnectionString("DefaultConnection"));

            AssemblyWatcher assemblyMonitor = new AssemblyWatcher(_pluginServices);

            CargaModulos(configuration.GetSection("ModulesEnable").GetChildren());
            ModulesInformation();

            Console.WriteLine("-> Enter status to know the status of the modules");
            Console.WriteLine("-> Enter test to inser data in log enables");
            Console.WriteLine("-> Enter exit to close the application");
            Console.WriteLine(string.Empty);
        }

        private static void CargaModulos(IEnumerable<IConfigurationSection> modulesConfig)
        {
            List<ModuleInfo> modules = new List<ModuleInfo>();
            foreach (var module in modulesConfig)
            {
                modules.Add(new ModuleInfo { Name = module.Key, Enable = Convert.ToBoolean(module.Value) });
            }
            GlobalModules.Modules = modules;

            Console.WriteLine(string.Empty);
        }

        private static void ModulesInformation()
        {
            Console.WriteLine("---> Modules Information");

            _pluginServices.LoadPlugins();
            foreach (var module in GlobalModules.Modules)
            {
                Console.WriteLine(
                    String.Format("|{0,-30}|{1,-30}|{2,-30}|", string.Concat("Module: ", module.Name), string.Concat("Enable: ", module.Enable), string.Concat("Exits: ", module.Exist)));
            }

            Console.WriteLine(string.Empty);
        }

        private static void Consola()
        {
            var input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "status":
                    ModulesInformation();
                    break;

                case "test":
                    TestModules();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }

            Consola();
        }

        private static void TestModules()
        {
            Console.WriteLine("---> Test");

            string type = string.Empty;
            string messege = string.Empty;

            Random rnd = new Random();
            switch (rnd.Next(1, 3))
            {
                case 1:
                    type = "Success";
                    messege = "Entiéndase por pluggable que el desarrollo";
                    break;

                case 2:
                    type = "Warning";
                    messege = "Advertencia de desbordamiento";
                    break;

                case 3:
                    type = "Error";
                    messege = "Error en la red";
                    break;
            }

            _pluginServices.ExcecuteLog(type, messege);

            Console.WriteLine(string.Empty);
        }
    }
}