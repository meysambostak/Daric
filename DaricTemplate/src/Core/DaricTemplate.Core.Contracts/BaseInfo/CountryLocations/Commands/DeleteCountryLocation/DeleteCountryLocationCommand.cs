

using Daric.Core.Infrastructure.Commands;

namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.DeleteCountryLocation;
public class DeleteCountryLocationCommand : ICommand<long>
{
    public long Id { get; set; }

}
