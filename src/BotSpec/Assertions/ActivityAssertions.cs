using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions
{
    public interface IActivityAssertions
    {
        IMessageAssertions Message();
    }

    public class ActivityAssertions : IActivityAssertions
    {
        private readonly Activity _activity;
        private readonly BotSpecSettings _settings;

        public ActivityAssertions(Activity activity, BotSpecSettings settings)
        {
            _activity = activity;
            _settings = settings;
        }

        public IMessageAssertions Message()
        {
            var message = _activity.AsMessageActivity();
            if (message == null)
                throw new BotSpecException($"Cannot convert activity to message as it's a {_activity.Type}.");

            return new MessageAssertions(message, _settings);
        }
    }
}