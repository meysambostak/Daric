

using Daric.Core.ApplicationServices.Commands;
using Daric.Core.Infrastructure.RequestResponse.Commands;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.CreateCountryLocation;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.CreateCountryLocation;
public class CreateCountryLocationCommandHandler : CommandHandler<CreateCountryLocationCommand, long>
{

    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;

    public CreateCountryLocationCommandHandler( 
                                               ICountryLocationCommandRepository countryLocationCommandRepository) 
    {
        _countryLocationCommandRepository = countryLocationCommandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateCountryLocationCommand command)
    {

        var result = new CommandResult<long>();
        var countryLocation = CountryLocation.Create(command.LocationType, command.Code, command.Title, command.AlternativeTitle,
                                                                 command.Abbreviation, command.ParentCountryLocationId);

        await _countryLocationCommandRepository.CreateAsync(countryLocation);
        result._data = countryLocation.Id;
        return result;
    }
}
