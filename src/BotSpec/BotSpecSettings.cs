using BotSpec.Client;

namespace BotSpec
{
    public class BotSpecSettings
    {
        public BotClientSettings BotClientSettings { get; set; } = new BotClientSettings();

        public static BotSpecSettings DefaultSettings = new BotSpecSettings();
    }
}