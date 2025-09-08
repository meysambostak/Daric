using Daric.Core.Infrastructure.Common;

namespace Daric.Core.Infrastructure.Commands;

/// https://github.com/vkhorikov/CqrsInPractice

public class CommandResult : ApplicationServiceResult
{

}

public class CommandResult<TData> : CommandResult
{
    public TData? _data;
    public TData? Data => _data;
}

