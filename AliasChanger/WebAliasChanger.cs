using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AliasChanger
{
    internal class WebAliasChanger
    {
        private readonly IServerConfigurationProvider serverConfigurationProvider;

        public WebAliasChanger(IServerConfigurationProvider serverConfigurationProvider)
        {
            this.serverConfigurationProvider = serverConfigurationProvider;
        }

        public void Run()
        {
            var serverName = AskForServer();
            string configPath;

            var serverConfiguration = serverConfigurationProvider.GetServerConfiguration();

            while (!serverConfiguration.TryGetValue(serverName, out configPath))
            {
                serverName = AskForServer();
            }

            var branding = AskForBranding();
            ChangeBranding(configPath, branding);

            Console.WriteLine("Branding changed");
        }

        private string AskForServer()
        {
            Console.Write("Please type server name: ");
            return Console.ReadLine();
        }

        private string AskForBranding()
        {
            Console.Write("Please type partner branding name: ");
            return Console.ReadLine();
        }

        private void ChangeBranding(string path, string branding)
        {
            string textToSearch = @"^\s*(<BrandingManagerSettings.*?>)";
            string textToReplace = $"<BrandingManagerSettings defaultProfile=\"{branding}\">";

            ReplaceInFile(path, textToSearch, textToReplace);
        }

        private void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            string content;

            using (var reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }

            content = Regex.Replace(content, searchText, replaceText, RegexOptions.Multiline);

            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(content);
            }
        }
    }
}