using Daric.Core.Infrastructure.Helpers.Guards;

namespace Daric.Core.Infrastructure.Helpers.Guards.GuardClauses;

public static class NullGuardClause
{
    public static void Null<T>(this Guard guard, T value, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value != null)
            throw new InvalidOperationException(message);
    }


    public static void NullArgument<T>(this Guard guard, T value, string name)
    { 

        if (value == null)
            throw new ArgumentNullException(name);
    }

    
}
