using BotSpec.Assertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;
using FluentAssertions;
using System;

namespace BotSpec.Test.Unit.AssertionTests.MessageAssertionTests
{
    public class When_asserting_message_text
    {
        private static readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

        [Fact]
        public void Matching_messages_does_not_throw()
        {
            var fixture = new Fixture();

            var message = fixture.Create<IMessageActivity>();
            var text = message.Text;
            
            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextMatches(text);
            act.ShouldNotThrow();
        }

        [Fact]
        public void Non_matching_message_does_throw()
        {
            var fixture = new Fixture();

            var message = fixture.Create<IMessageActivity>();
            var text = "Non matching text";
            
            var sut = new MessageAssertions(message, _settings);

            Action act = () => sut.TextMatches(text);
            act.ShouldThrow<BotSpecException>();
        }
    }
}