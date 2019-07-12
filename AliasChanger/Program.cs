using System;
using System.IO;

namespace AliasChanger
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\servers.json");
            IServerConfigurationProvider serverConfigurationProvider = new ServerConfiguratioFromConfigProvider(configPath);
            var webAliasChanger = new WebAliasChanger(serverConfigurationProvider);

            webAliasChanger.Run();
        }
    }
}
