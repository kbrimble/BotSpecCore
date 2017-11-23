using FluentAssertions;
using Xunit;

namespace BotSpec.Test.Unit.StringMatchingTests
{
    public class When_matching_patterns
    {
        [Fact]
        public void Exact_text_match_with_no_special_characters_should_return_true()
        {
            var input = "Hello";
            var pattern = "Hello";

            StringMatching.MatchesPattern(input, pattern).Should().BeTrue();
        }

        [Fact]
        public void Non_matching_text_with_no_special_characters_should_return_false()
        {
            var input = "Hello";
            var pattern = "Goodbye";

            StringMatching.MatchesPattern(input, pattern).Should().BeFalse();            
        }

        [Fact]
        public void Matches_with_valid_regexes_should_return_true()
        {
            var input = "Hello";
            var pattern = "[A-Za-z]*";

            StringMatching.MatchesPattern(input, pattern).Should().BeTrue();
        }

        [Fact]
        public void Patterns_that_do_not_match_input_should_return_false()
        {
            var input = "Hello";
            var pattern = "^[a]*$";

            StringMatching.MatchesPattern(input, pattern).Should().BeFalse();            
        }

        [Fact]
        public void Pattern_matching_is_case_insensitive()
        {
            var input = "HELLO";
            var pattern = "[a-z]*";

            StringMatching.MatchesPattern(input, pattern).Should().BeTrue();
        }
    }
}