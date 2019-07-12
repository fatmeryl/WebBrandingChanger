using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AliasChanger
{
    public class ServerConfiguratioFromConfigProvider : IServerConfigurationProvider
    {
        private readonly string configPath;

        public ServerConfiguratioFromConfigProvider(string configPath)
        {
            this.configPath = configPath;
        }

        public Dictionary<string, string> GetServerConfiguration()
        {
            var json = File.ReadAllText(this.configPath);

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return values;
        }
    }
}