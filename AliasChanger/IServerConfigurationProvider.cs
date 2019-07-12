using System.Collections.Generic;

namespace AliasChanger
{
    public interface IServerConfigurationProvider
    {
        Dictionary<string, string> GetServerConfiguration();
    }
}