using Daric.Core.Infrastructure.RequestResponse.Queries;

namespace Daric.Core.Contracts.ApplicationServices.Queries;
 
public interface IQueryHandler<TQuery, TData>
    where TQuery : class, IQuery<TData>
{
    Task<QueryResult<TData>> Handle(TQuery request);
}

