using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BotSpec.Client
{
    internal class DefaultBotClient : IBotClient
    {
        private readonly IDirectLineClient _directLineClient;
        private readonly ILogger<DefaultBotClient> _logger;
        private readonly BotClientSettings _settings;

        private Conversation _conversation;
        private ChannelAccount _channelAccount;
        private string _higherWatermark;
        private string _lowerWatermark;

        public DefaultBotClient(IDirectLineClient directLineClient, BotClientSettings settings, ILogger<DefaultBotClient> logger)
        {
            _settings = settings;
            _directLineClient = directLineClient;
            _logger = logger;
        }

        public void StartConversation()
        {
            _conversation = _directLineClient.Conversations.StartConversation();
            _channelAccount = new ChannelAccount($"BotSpec{_conversation.ConversationId.Substring(0, 8)}");
            _logger.LogInformation($"Conversation created with channel account {_channelAccount.Id}");
        }

        public void SendMessage(string messageText)
        {
            var message = new Activity
            {
                Text = messageText,
                From = _channelAccount,
                Type = "message"
            };
            SendActivity(message);
        }

        public void SendActivity(Activity activity)
        {
            _directLineClient.Conversations.PostActivityAsync(_conversation.ConversationId, activity);
            _logger.LogInformation($"Activity sent with text: {activity.Text}");
            _logger.LogDebug($"Full activity: {JsonConvert.SerializeObject(activity)}");
        }

        public IList<Activity> GetActivitiesFromHigherWatermark(int expectedNumberOfActivities)
        {
            var activities = GetActivitiesWithRetry(expectedNumberOfActivities, _higherWatermark);

            if (activities?.Watermark == null)
                return activities?.Activities;

            _lowerWatermark = _higherWatermark;
            _higherWatermark = activities.Watermark;

            return activities.Activities;
        }

        public IList<Activity> GetActivitiesFromLowerWatermark(int expectedNumberofActivities)
            => GetActivitiesWithRetry(expectedNumberofActivities, _lowerWatermark).Activities;

        private ActivitySet GetActivitiesWithRetry(int expectedNumberOfActivities, string watermark)
        {
            var noOfRetries = _settings.RetryTimes;
            ActivitySet activitySet = null;
            var retry = true;
            while (noOfRetries > 0 && retry)
            {
                _logger.LogDebug($"Getting latest activity set. Will try {noOfRetries} times.");
                var latestSet = GetActivitySet(watermark);
                var newActivities = NumberOfNewActivities(latestSet, watermark);
                _logger.LogDebug($"{newActivities} new activities since last retrieval");

                if (expectedNumberOfActivities > 0 && newActivities < expectedNumberOfActivities)
                {
                    _logger.LogDebug($@"Expected {expectedNumberOfActivities} but found {newActivities}. 
                         Waiting {_settings.RetryWaitTimeMilliseconds}ms for new activities");
                    noOfRetries--;
                    Task.Delay(_settings.RetryWaitTimeMilliseconds).Wait();
                    continue;
                }

                activitySet = latestSet;

                retry = false;
            }
            return activitySet;
        }

        private ActivitySet GetActivitySet(string watermark)
            => _directLineClient.Conversations.GetActivities(_conversation.ConversationId, watermark);

        private static int NumberOfNewActivities(ActivitySet activitySet, string watermark)
        {
            if (activitySet?.Watermark == null)
                return 0;
            return int.Parse(activitySet.Watermark ?? "0") - int.Parse(watermark ?? "0");
        }
    }
}