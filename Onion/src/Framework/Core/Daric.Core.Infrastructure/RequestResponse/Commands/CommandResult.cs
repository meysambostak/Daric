using Daric.Core.Infrastructure.RequestResponse.Common;

namespace Daric.Core.Infrastructure.RequestResponse.Commands;

/// https://github.com/vkhorikov/CqrsInPractice

public class CommandResult : ApplicationServiceResult
{

}

public class CommandResult<TData> : CommandResult
{
    public TData? _data;
    public TData? Data => _data;
}

