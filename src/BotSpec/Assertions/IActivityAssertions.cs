namespace BotSpec.Assertions
{
    public interface IActivityAssertions
    {
        IActivityAssertions TypeIs(string activityType);
        IActivityAssertions TypeLike(string activityTypePattern);
        IActivityAssertions IdIs(string id);
        IActivityAssertions IdLike(string idPattern);
        IActivityAssertions ChannelIdIs(string channelId);
        IActivityAssertions ChannelIdLike(string channelIdPattern);
        IMessageAssertions IsMessage();
    }
}