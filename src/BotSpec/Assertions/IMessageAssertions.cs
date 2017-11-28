using System.Collections.Generic;

namespace BotSpec.Assertions
{
    public interface IMessageAssertions
    {
        IMessageAssertions TextIs(string shouldMatch);
        IMessageAssertions TextLike(string pattern);
        IMessageAssertions SummaryIs(string shouldMatch);
        IMessageAssertions SummaryLike(string pattern);
    }
}