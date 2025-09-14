using Daric.Core.ApplicationServices.Queries;
using Daric.Core.Infrastructure.RequestResponse.Queries;

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;

namespace DaricTemplate.Core.ApplicationService.BaseInfo.CountryLocations.Queries.GetById;

 


public class GetCountryLocationByIdQueryHandler : QueryHandler<GetCountryLocationByIdQuery, CountryLocationDto?>
{
    private readonly ICountryLocationQueryRepository _countryLocationQueryRepository;

    public GetCountryLocationByIdQueryHandler(
                                   ICountryLocationQueryRepository countryLocationQueryRepository)  
    {
        _countryLocationQueryRepository = countryLocationQueryRepository;
    }

    public override async Task<QueryResult<CountryLocationDto?>> Handle(GetCountryLocationByIdQuery query)
    {
        var blog = await _countryLocationQueryRepository.ExecuteAsync(query);

        return Result(blog);
    }
}