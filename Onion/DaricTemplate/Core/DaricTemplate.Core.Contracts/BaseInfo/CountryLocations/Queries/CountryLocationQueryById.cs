using Daric.Core.Infrastructure.RequestResponse.Endpoints;
using Daric.Core.Infrastructure.RequestResponse.Queries;

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries;

public class GetCountryLocationByIdQuery : IQuery<CountryLocationDto?> 
{
    public long Id { get; set; } 
}
 
