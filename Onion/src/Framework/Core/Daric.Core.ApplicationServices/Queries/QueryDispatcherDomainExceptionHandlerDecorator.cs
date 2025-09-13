using Daric.Core.Domain.Exceptions;
using Daric.Core.Infrastructure.Abstractions.Logger;
using Daric.Core.Infrastructure.RequestResponse.Common;
using Daric.Core.Infrastructure.RequestResponse.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Daric.Core.ApplicationServices.Queries;

public class QueryDispatcherDomainExceptionHandlerDecorator : QueryDispatcherDecorator
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueryDispatcherDomainExceptionHandlerDecorator> _logger;
    public override int Order => 2;
    #endregion

    #region Constructors
    public QueryDispatcherDomainExceptionHandlerDecorator(IServiceProvider serviceProvider,
                                                          ILogger<QueryDispatcherDomainExceptionHandlerDecorator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    #endregion

    #region Query Dispatcher
    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        try
        {
            QueryResult<TData> result = await _queryDispatcher.Execute<TQuery, TData>(query);
            return result;

        }
        catch (DomainStateException ex)
        {
            _logger.LogError(FrameworkEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
            return DomainExceptionHandlingWithReturnValue<TQuery, TData>(ex);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(FrameworkEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
                return DomainExceptionHandlingWithReturnValue<TQuery, TData>(domainStateException);
            }
            throw;
        }
    }
    #endregion

    #region Privaite Methods
    private QueryResult<TData> DomainExceptionHandlingWithReturnValue<TQuery, TData>(DomainStateException ex)
    {
        var queryResult = new QueryResult<TData>()
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        queryResult.AddMessage(GetExceptionText(ex));

        return queryResult;
    }

    private string GetExceptionText(DomainStateException domainStateException)
    { 

        string? result = domainStateException?.Parameters.Any() == true ?
             string.Format(domainStateException.Message, domainStateException.Parameters) :
               domainStateException?.Message;

        _logger.LogInformation(FrameworkEventId.DomainValidationException, "Domain Exception message is {DomainExceptionMessage}", result);

        return result;
    }
    #endregion
}
