using System;
using BotSpec.Assertions;
using Microsoft.Bot.Connector.DirectLine;
using Ploeh.AutoFixture;
using Xunit;
using FluentAssertions;

namespace BotSpec.Test.Unit.AssertionTests.ActivityAssertionTests
{
    public class When_asserting_channelId
    {
        private readonly BotSpecSettings _settings = BotSpecSettings.DefaultSettings;

        [Fact]
        public void Exact_match_does_not_throw()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            Action act = () => sut.ChannelIdIs(activity.ChannelId);
            act.ShouldNotThrow();
        }

        [Fact]
        public void Non_exact_match_throws()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            Action act = () => sut.ChannelIdIs("NonMatchingId");
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Pattern_match_does_not_throw()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            Action act = () => sut.ChannelIdLike(".*");
            act.ShouldNotThrow();
        }

        [Fact]
        public void Pattern_that_does_not_match_throws()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            Action act = () => sut.ChannelIdLike(".*NonMatchingPattern.*");
            act.ShouldThrowExactly<BotSpecException>();
        }

        [Fact]
        public void Is_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            var result = sut.ChannelIdIs(activity.ChannelId);
            result.Should().Be(sut);
        }

        [Fact]
        public void Like_returns_same_object_for_fluent_chaining()
        {
            var fixture = new Fixture();
            var activity = fixture.Create<Activity>();

            var sut = new ActivityAssertions(activity, _settings);
            
            var result = sut.ChannelIdLike($"^{activity.ChannelId}$");
            result.Should().Be(sut);
        }
    }
}