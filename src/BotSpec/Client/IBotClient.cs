using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Client
{
    public interface IBotClient
    {
        void StartConversation();
        void SendMessage(string messageText);
        void SendActivity(Activity message);
        IList<Activity> GetActivitiesFromHigherWatermark(int expectedNumberofActivities);
        IList<Activity> GetActivitiesFromLowerWatermark(int expectedNumberofActivities);
    }
}