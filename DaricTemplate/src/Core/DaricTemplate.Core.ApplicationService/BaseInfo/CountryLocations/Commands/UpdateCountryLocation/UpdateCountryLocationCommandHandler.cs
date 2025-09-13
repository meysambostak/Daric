
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using Daric.Core.ApplicationServices.Commands; 
using Daric.Extensions.ObjectMappers.AutoMapper.Services;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.UpdateCountryLocation;
using Daric.Core.Infrastructure.RequestResponse.Commands;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.UpdateCountryLocation;

public class UpdateCountryLocationCommandHandler : CommandHandler<UpdateCountryLocationCommand, UpdateCountryLocationCommand>
{

    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;
    private readonly IMapperAdapter _mapper;

    public UpdateCountryLocationCommandHandler(
                                               ICountryLocationCommandRepository countryLocationCommandRepository, 
                                               IMapperAdapter mapper) 
    {
        _countryLocationCommandRepository = countryLocationCommandRepository;
        _mapper = mapper;
    }

    public override async Task<CommandResult<UpdateCountryLocationCommand>> Handle(UpdateCountryLocationCommand command)
    {

        var result = new CommandResult<UpdateCountryLocationCommand>();
        CountryLocation countryLocation = _mapper.Map<UpdateCountryLocationCommand, CountryLocation>(command);
        Task<CountryLocation> updatecountryLocation = _countryLocationCommandRepository.UpdateAsync(countryLocation);
        result._data = command;
        return result;
    }
}
