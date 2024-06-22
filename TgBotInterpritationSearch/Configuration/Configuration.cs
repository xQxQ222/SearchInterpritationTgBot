using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotInterpritationSearch.Configuration
{
    public class Configuration
    {
        public static StorageTgBotSets BotSettings { get; private set; } = new StorageTgBotSets();
        public static void SetProperties(IConfiguration configuration)
        {
            BotSettings = GetSection<StorageTgBotSets>(configuration, "StorageTgBotSets");
        }
        private static T GetSection<T>(IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection(sectionName).Get<T>()
                ?? throw new InvalidOperationException($"Not found section {nameof(T)} in configuration.");
        }
    }
}
