using BotSpec;
using FluentAssertions;
using Xunit;

namespace BotSpec.Test.Unit.StringMatchingTests
{
    public class When_matching_strings
    {
        [Fact]
        public void Should_return_true_if_strings_exactly_match()
        {
            var input = "Hello";
            var matches = "Hello";

            StringMatching.Matches(input, matches).Should().BeTrue();
        }

        [Fact]
        public void Should_return_false_if_strings_dont_exactly_match()
        {
            var input = "Hello";
            var doesntMatch = "Bye";

            StringMatching.Matches(input, doesntMatch).Should().BeFalse();
        }

        [Fact]
        public void Matches_should_be_case_insensitive()
        {
            var input = "Hello";
            var ignoreCaseMatch = "HELLO";

            StringMatching.Matches(input, ignoreCaseMatch).Should().BeTrue();
        }
    }
}