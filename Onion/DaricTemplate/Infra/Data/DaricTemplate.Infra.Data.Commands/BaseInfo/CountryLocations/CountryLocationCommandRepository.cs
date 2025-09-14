

using Daric.Core.Contracts.Data.Commands;
using Daric.Infra.Data.Commands;

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using DaricTemplate.Infra.Data.Commands.Context;

using Microsoft.EntityFrameworkCore;

namespace DaricTemplate.Infra.Data.Commands.BaseInfo.CountryLocations;


public class CountryLocationCommandRepository :
BaseCommandRepository<CountryLocation, DaricTemplateCommandDbContext, long>,
ICountryLocationCommandRepository
{
    private readonly DbSet<CountryLocation> _countryLocations;
    private readonly IUnitOfWork _unitOfWork;
    
    public CountryLocationCommandRepository(DaricTemplateCommandDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
    {
        _unitOfWork = unitOfWork;
        _countryLocations = dbContext.CountryLocations;
    }
    
    public ValueTask<CountryLocation> FindCountryLocationAsync(long countryLocationId) => _countryLocations.FindAsync(countryLocationId);

    public async Task CreateAsync(CountryLocation countryLocation)
    {
        await _countryLocations.AddAsync(countryLocation);
        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task<CountryLocation> UpdateAsync(CountryLocation countryLocation)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<CountryLocation> countryLocationResult =
            _countryLocations.Update(countryLocation);
        await _unitOfWork.SaveChangesAsync();
        return countryLocationResult.Entity;
    }
}
