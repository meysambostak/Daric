


using Daric.Core.Infrastructure.RequestResponse.Commands;

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.CreateCountryLocation;
public class CreateCountryLocationCommand : ICommand<long>
{
    public byte LocationType { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string AlternativeTitle { get; set; }
    public string Abbreviation { get; set; } 
    public Guid? ParentCountryLocationId { get; set; }

}
