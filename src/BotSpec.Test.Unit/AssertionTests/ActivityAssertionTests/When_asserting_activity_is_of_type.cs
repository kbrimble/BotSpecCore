using System;
using BotSpec.Assertions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;

namespace BotSpec.Test.Unit.AssertionTests.ActivityAssertionTests
{
    public class When_asserting_activity_is_of_type
    {
        public class And_activity_is_of_that_type
        {
            private readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

            [Fact]
            public void A_message_returns_IMessageAssertions()
            {
                var fixture = new Fixture();
                var activity = fixture.Create<Activity>();
                activity.Type = ActivityTypes.Message;

                var sut = new ActivityAssertions(activity, _settings);

                var result = sut.IsMessage();
                result.Should().BeAssignableTo<IMessageAssertions>();
            }
        }

        public class And_activity_is_not_of_that_type
        {
            private readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

            [Fact]
            public void Non_messages_throw_when_trying_to_convert_to_IMessageAssertions()
            {
                var fixture = new Fixture();
                var activity = fixture.Create<Activity>();
                activity.Type = ActivityTypes.EndOfConversation;

                var sut = new ActivityAssertions(activity, _settings);

                Action act = () => sut.IsMessage();
                act.ShouldThrowExactly<BotSpecException>();
            }
        }
    }
}