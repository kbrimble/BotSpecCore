using System;
using BotSpec.Assertions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;

namespace BotSpec.Test.Unit.AssertionTests.MessageAssertionTests
{
    public class When_asserting_summary
    {
        private static readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

        [Fact]
        public void Exact_match_does_not_throw()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = message.Summary;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.SummaryIs(summary);
            act.ShouldNotThrow();
        }

        [Fact]
        public void Non_exact_match_throws()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = "Non matching summary";

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.SummaryIs(summary);
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Pattern_match_does_not_throw()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = message.Summary;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.SummaryLike($"^{summary}$");
            act.ShouldNotThrow();
        }

        [Fact]
        public void Pattern_that_does_not_match_throws()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = message.Summary;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.SummaryLike($"^this doesnt match$");
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Is_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = message.Summary;

            var sut = new MessageAssertions(message, _settings);

            var result = sut.SummaryIs(summary);
            result.Should().Be(sut);
        }

        [Fact]
        public void Like_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var summary = message.Summary;

            var sut = new MessageAssertions(message, _settings);

            var result = sut.SummaryLike($"^{summary}$");
            result.Should().Be(sut);
        }

    }
}