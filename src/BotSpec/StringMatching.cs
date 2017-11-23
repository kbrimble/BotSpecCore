using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BotSpec
{
    public static class StringMatching
    {
        public static bool Matches(string original, string shouldMatch)
            => original.Equals(shouldMatch, StringComparison.OrdinalIgnoreCase);

        public static bool MatchesPattern(string original, string pattern)
            => Regex.IsMatch(original, pattern, RegexOptions.IgnoreCase);

        public static (bool doesMatch, IEnumerable<string> matches) MatchesPatternWithGroups(string original, string pattern)
        {
            var matchedGroups = Enumerable.Empty<string>();

            var isMatch = MatchesPattern(original, pattern);
            if (!isMatch)
                return (false, matchedGroups);

            var matches = Regex.Matches(original.ToLowerInvariant(), pattern).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return (true, matchedGroups);
        }
    }
}