using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions
{
    public class MessageAssertions : ActivityAssertions, IMessageAssertions
    {
        private readonly IMessageActivity _message;
        private readonly BotSpecSettings _settings;

        public MessageAssertions(IActivity activity, BotSpecSettings settings) : base(activity, settings)
        {
            var message = activity.AsMessageActivity();
            if (message == null)
                throw new BotSpecException($"Could not convert activity to a message. This activity is a {activity.Type}.");
            _message = message;
            _settings = settings;
        }

        public IMessageAssertions TextIs(string shouldMatch)
        {
            if (!StringMatching.Matches(_message.Text, shouldMatch))
                ThrowMatchException(nameof(_message.Text), shouldMatch, _message.Text);

            return this;
        }

        public IMessageAssertions TextLike(string pattern)
        {
            if (!StringMatching.MatchesPattern(_message.Text, pattern))
                ThrowPatternException(nameof(_message.Text), pattern, _message.Text);

            return this;
        }

        public IMessageAssertions SummaryIs(string shouldMatch)
        {
            if (!StringMatching.Matches(_message.Summary, shouldMatch))
                ThrowMatchException(nameof(_message.Summary), shouldMatch, _message.Summary);

            return this;
        }

        public IMessageAssertions SummaryLike(string pattern)
        {
            if (!StringMatching.MatchesPattern(_message.Summary, pattern))
                ThrowPatternException(nameof(_message.Summary), pattern, _message.Summary);

            return this;
        }
    }
}