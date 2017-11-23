using System.Collections.Generic;

namespace BotSpec.Assertions
{
    public interface IMessageAssertions
    {
        IMessageAssertions TextMatches(string shouldMatch);
        IMessageAssertions TextMatchesPattern(string pattern);
        IMessageAssertions TextMatchesPatternWithGroups(string pattern, out IEnumerable<string> matches);
        IMessageAssertions SummaryMatches(string shouldMatch);
        IMessageAssertions SummaryMatchesPattern(string pattern);
        IMessageAssertions SummaryMatchesPatternWithGroups(string pattern, out IEnumerable<string> matches);
    }
}