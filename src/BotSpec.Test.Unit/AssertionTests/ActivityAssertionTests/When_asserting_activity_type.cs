using System;
using BotSpec.Assertions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;

namespace BotSpec.Test.Unit.AssertionTests.ActivityAssertionTests
{
    public class When_asserting_activity_type
    {
        private readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

        [Fact]
        public void Exact_match_does_not_throw()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();
            var type = ActivityTypes.ConversationUpdate;
            activity.Type = type;

            var sut = new ActivityAssertions(activity, _settings);

            Action act = () => sut.TypeIs(type);
            act.ShouldNotThrow();
        }

        [Fact]
        public void Non_exact_match_throws()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();
            activity.Type = ActivityTypes.ConversationUpdate;

            var sut = new ActivityAssertions(activity, _settings);

            Action act = () => sut.TypeIs(ActivityTypes.DeleteUserData);
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Pattern_match_does_not_throw()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();
            activity.Type = ActivityTypes.ConversationUpdate;

            var sut = new ActivityAssertions(activity, _settings);

            Action act = () => sut.TypeLike("^.*Update$");
            act.ShouldNotThrow();
        }

        [Fact]
        public void Pattern_that_does_not_match_throws()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();
            activity.Type = ActivityTypes.ConversationUpdate;

            var sut = new ActivityAssertions(activity, _settings);

            Action act = () => sut.TypeLike("^.*Delete$");
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Is_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            var result = sut.TypeIs(activity.Type);
            result.Should().Be(sut);
        }

        [Fact]
        public void Like_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            var result = sut.TypeLike($"^{activity.Type}$");
            result.Should().Be(sut);
        }
    }
}