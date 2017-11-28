using BotSpec.Assertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;
using FluentAssertions;
using System;

namespace BotSpec.Test.Unit.AssertionTests.MessageAssertionTests
{
    public class When_asserting_text
    {
        private static readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

        [Fact]
        public void Exact_match_does_not_throw()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = message.Text;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextIs(text);
            act.ShouldNotThrow();
        }

        [Fact]
        public void Non_exact_match_throws()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = "Non matching text";

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextIs(text);
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Pattern_match_does_not_throw()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = message.Text;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextLike($"^{text}$");
            act.ShouldNotThrow();
        }

        [Fact]
        public void Pattern_that_does_not_match_throws()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = message.Text;

            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextLike($"^this doesnt match$");
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Is_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = message.Text;

            var sut = new MessageAssertions(message, _settings);

            var result = sut.TextIs(text);
            result.Should().Be(sut);
        }

        [Fact]
        public void Like_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();

            IMessageActivity message = fixture.Create<Activity>();
            message.Type = ActivityTypes.Message;
            var text = message.Text;

            var sut = new MessageAssertions(message, _settings);

            var result = sut.TextLike($"^{text}$");
            result.Should().Be(sut);
        }
    }
}