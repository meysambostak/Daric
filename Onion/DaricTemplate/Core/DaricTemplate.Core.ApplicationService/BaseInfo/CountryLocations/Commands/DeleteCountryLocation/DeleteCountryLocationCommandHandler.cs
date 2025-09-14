
using Daric.Core.ApplicationServices.Commands;
using Daric.Core.Infrastructure.RequestResponse.Commands;

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.DeleteCountryLocation;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.DeleteCountryLocation;
public class DeleteCountryLocationCommandHandler : CommandHandler<DeleteCountryLocationCommand, long>
{
    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;

    public DeleteCountryLocationCommandHandler(
                                               ICountryLocationCommandRepository countryLocationCommandRepository) 
    {
        _countryLocationCommandRepository = countryLocationCommandRepository;
    }

    public override async Task<CommandResult<long>> Handle(DeleteCountryLocationCommand command)
    {
        var result = new CommandResult<long>();
        _countryLocationCommandRepository.Delete(command.Id);
        await _countryLocationCommandRepository.SaveChangesAsync();
        result._data = command.Id;
        return result;
    }
}
