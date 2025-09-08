using Daric.Core.Infrastructure.Common;

namespace Daric.Core.Infrastructure.Queries;

 
public sealed class QueryResult<TData> : ApplicationServiceResult
{
    public TData? _data;
    public TData? Data => _data;
}

