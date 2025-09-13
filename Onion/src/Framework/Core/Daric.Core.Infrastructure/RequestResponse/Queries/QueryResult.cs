using Daric.Core.Infrastructure.RequestResponse.Common;

namespace Daric.Core.Infrastructure.RequestResponse.Queries;

 
public sealed class QueryResult<TData> : ApplicationServiceResult
{
    public TData? _data;
    public TData? Data => _data;
}

