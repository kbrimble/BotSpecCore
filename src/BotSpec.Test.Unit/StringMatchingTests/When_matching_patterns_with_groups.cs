using FluentAssertions;
using Xunit;

namespace BotSpec.Test.Unit.StringMatchingTests
{
    public class When_matching_patterns_with_groups
    {
        [Fact]
        public void Exact_text_match_with_no_special_characters_should_return_true_with_no_matches()
        {
            var input = "Hello";
            var pattern = "Hello";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);

            result.doesMatch.Should().BeTrue();
            result.matches.Should().BeEmpty();
        }

        [Fact]
        public void Non_matching_text_with_no_special_characters_should_return_false()
        {
            var input = "Hello";
            var pattern = "Goodbye";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeFalse();            
        }

        [Fact]
        public void Matches_with_valid_regexes_should_return_true()
        {
            var input = "Hello";
            var pattern = "[A-Za-z]*";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeTrue();
        }

        [Fact]
        public void Patterns_that_do_not_match_input_should_return_false()
        {
            var input = "Hello";
            var pattern = "^[a]*$";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeFalse();            
        }

        [Fact]
        public void Pattern_matching_is_case_insensitive()
        {
            var input = "HELLO";
            var pattern = "[a-z]*";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeTrue();
        }

        [Fact]
        public void Matching_pattern_with_groupings_should_return_the_matched_groups()
        {
            var input = "hello";
            var pattern = "([a-z]*)";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeTrue();
            result.matches.Should().Contain(input);
        }

        [Fact]
        public void Multiple_matching_groups_should_all_be_returned()
        {
            var input = "hello";
            var pattern = "^([a-z])([a-z]*)$";

            var result = StringMatching.MatchesPatternWithGroups(input, pattern);
            result.doesMatch.Should().BeTrue();
            result.matches.Should().Contain("h");
            result.matches.Should().Contain("ello");
        }
    }
}