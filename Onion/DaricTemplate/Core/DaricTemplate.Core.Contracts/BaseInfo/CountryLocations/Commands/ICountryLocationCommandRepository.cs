

using Daric.Core.Contracts.Data.Commands;

using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands;
public interface ICountryLocationCommandRepository : ICommandRepository<CountryLocation,long>
{
    ValueTask<CountryLocation> FindCountryLocationAsync(long countryLocationId);
    // Task<CountryLocation> FindByUsernameAsync(string roleName);
    Task CreateAsync(CountryLocation countryLocation);
    Task<CountryLocation> UpdateAsync(CountryLocation countryLocation);
}
