using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions
{
    public class MessageAssertions : IMessageAssertions
    {
        private readonly IMessageActivity _message;
        private readonly BotSpecSettings _settings;

        public MessageAssertions(IMessageActivity message, BotSpecSettings settings)
        {
            _message = message;
            _settings = settings;
        }

        public IMessageAssertions TextMatches(string shouldMatch)
        {
            if (!StringMatching.Matches(_message.Text, shouldMatch))
                throw new BotSpecException($"Message text did not match expected text."
                + " Text: \"{_message.Text}\" Expected: \"{shouldMatch}\"");

            return this;
        }

        public IMessageAssertions TextMatchesPattern(string pattern)
        {
            if (!StringMatching.MatchesPattern(_message.Text, pattern))
                throw new BotSpecException($"Message text did not match pattern."
                + " Text: \"{_message.Text}\" Pattern: \"{pattern}\"");

            return this;
        }

        public IMessageAssertions TextMatchesPatternWithGroups(string pattern, out IEnumerable<string> matches)
        {
            matches = null;

            var matchWithGroups = StringMatching.MatchesPatternWithGroups(_message.Text, pattern);

            if (!matchWithGroups.doesMatch)
                throw new BotSpecException($"Message text did not match pattern."
                + " Text: \"{_message.Text}\" Pattern: \"{pattern}\"");

            matches = matchWithGroups.matches;

            return this;
        }

        public IMessageAssertions SummaryMatches(string shouldMatch)
        {
            if (!StringMatching.Matches(_message.Summary, shouldMatch))
                throw new BotSpecException($"Message summary did not match expected text."
                + " Summary: \"{_message.Summary}\" Expected: \"{shouldMatch}\"");

            return this;
        }

        public IMessageAssertions SummaryMatchesPattern(string pattern)
        {
            if (!StringMatching.MatchesPattern(_message.Summary, pattern))
                throw new BotSpecException($"Message summary did not match pattern."
                + " Summary: \"{_message.Summary}\" Pattern: \"{pattern}\"");

            return this;
        }

        public IMessageAssertions SummaryMatchesPatternWithGroups(string pattern, out IEnumerable<string> matches)
        {
            matches = null;

            var matchWithGroups = StringMatching.MatchesPatternWithGroups(_message.Summary, pattern);

            if (!matchWithGroups.doesMatch)
                throw new BotSpecException($"Message summary did not match pattern."
                + " Summary: \"{_message}\" Pattern: \"{pattern}\"");

            matches = matchWithGroups.matches;

            return this;
        }
    }
}