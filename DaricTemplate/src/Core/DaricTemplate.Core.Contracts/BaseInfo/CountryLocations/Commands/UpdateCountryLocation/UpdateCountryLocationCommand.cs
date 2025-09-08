using Daric.Core.Infrastructure.Commands;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.UpdateCountryLocation;
public class UpdateCountryLocationCommand : ICommand<UpdateCountryLocationCommand>
{
    public long Id { get; set; }
    public LocationType LocationType { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string AlternativeTitle { get; set; }
    public string Abbreviation { get; set; }
    public bool? IsPort { get; set; }
    public Guid? ParentCountryLocationId { get; set; }
}
