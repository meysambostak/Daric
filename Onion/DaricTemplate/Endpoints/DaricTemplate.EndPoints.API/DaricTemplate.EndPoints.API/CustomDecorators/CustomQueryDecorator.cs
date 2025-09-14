

using Daric.Core.ApplicationServices.Queries;
using Daric.Core.Infrastructure.RequestResponse.Queries;

namespace DaricTemplate.EndPoints.API.CustomDecorators;

public class CustomQueryDecorator : QueryDispatcherDecorator
{
    public override int Order => 0;

    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        return await _queryDispatcher.Execute<TQuery, TData>(query);
    }
}
