using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions
{
    public class ActivityAssertions : IActivityAssertions
    {
        private readonly IActivity _activity;
        private readonly BotSpecSettings _settings;

        public ActivityAssertions(IActivity activity, BotSpecSettings settings)
        {
            _activity = activity;
            _settings = settings;
        }

        public IActivityAssertions ChannelIdIs(string channelId)
        {
            if (!StringMatching.Matches(_activity.ChannelId, channelId))
                ThrowMatchException(nameof(_activity.ChannelId), channelId, _activity.ChannelId);

            return this;
        }

        public IActivityAssertions ChannelIdLike(string channelIdPattern)
        {
            if (!StringMatching.MatchesPattern(_activity.ChannelId, channelIdPattern))
                ThrowPatternException(nameof(_activity.ChannelId), channelIdPattern, _activity.ChannelId);

            return this;
        }

        public IActivityAssertions IdIs(string id)
        {
            if (!StringMatching.Matches(_activity.Id, id))
                ThrowMatchException(nameof(_activity.Id), id, _activity.Id);

            return this;
        }

        public IActivityAssertions IdLike(string idPattern)
        {
            if (!StringMatching.MatchesPattern(_activity.Id, idPattern))
                ThrowPatternException(nameof(_activity.Id), idPattern, _activity.Id);

            return this;
        }

        public IActivityAssertions TypeIs(string activityType)
        {
            if (!StringMatching.Matches(_activity.Type, activityType))
                ThrowMatchException(nameof(_activity.Type), activityType, _activity.Type);

            return this;
        }

        public IActivityAssertions TypeLike(string activityTypePattern)
        {
            if (!StringMatching.MatchesPattern(_activity.Type, activityTypePattern))
                ThrowPatternException(nameof(_activity.Type), activityTypePattern, _activity.Type);

            return this;
        }

        public IMessageAssertions IsMessage()
        {
            return new MessageAssertions(_activity, _settings);
        }

        protected void ThrowMatchException(string propertyName, string expected, string actual)
            => throw new BotSpecException($"{propertyName} did not match expected. Expected: \"{expected}\" Actual: \"{actual}\"");

        protected void ThrowPatternException(string propertyName, string pattern, string actual)
            => throw new BotSpecException($"{propertyName} did not match pattern. Pattern: \"{pattern}\" Actual: \"{actual}\"");
    }
}