using Daric.Infra.Data.Queries;

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;
using DaricTemplate.Core.Contracts.Common.Queries;
using DaricTemplate.Infra.Data.Queries.Context;

using Microsoft.EntityFrameworkCore;

namespace DaricTemplate.Infra.Data.Queries.BaseInfo.CountryLocations;

public class CountryLocationQueryRepository : BaseQueryRepository<DaricTemplateQueryDbContext>, ICountryLocationQueryRepository
{
    public CountryLocationQueryRepository(DaricTemplateQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CountryLocationDto?> ExecuteAsync(GetCountryLocationByIdQuery query)
        => await _dbContext.CountryLocations.Select(c => new CountryLocationDto(

             c.Id,
            c.LocationType,
             c.Code,
             c.Title,
             c.AlternativeTitle,
             c.Abbreviation
        )).FirstOrDefaultAsync(c => c.Id.Equals(query.Id));

    public Task<CountryLocationDto?> ExecuteAsync(GetByIdQuery<Core.Domain.BaseInfo.CountryLocations.Entities.CountryLocation> query)
    {
        throw new NotImplementedException();
    }
}
