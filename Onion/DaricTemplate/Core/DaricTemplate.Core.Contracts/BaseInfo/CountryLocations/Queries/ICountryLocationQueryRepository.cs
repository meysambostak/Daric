using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs; 

 

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries;

public interface ICountryLocationQueryRepository
{
    public Task<CountryLocationDto?> ExecuteAsync(GetCountryLocationByIdQuery query);
}
