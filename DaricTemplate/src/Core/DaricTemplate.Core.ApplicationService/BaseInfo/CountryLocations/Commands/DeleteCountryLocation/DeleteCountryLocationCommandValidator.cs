using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.DeleteCountryLocation;
using FluentValidation;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.DeleteCountryLocation;

public class DeleteCountryLocationCommandValidator : AbstractValidator<DeleteCountryLocationCommand>
{
    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;
    public DeleteCountryLocationCommandValidator(ICountryLocationCommandRepository countryLocationCommandRepository)
    {
        _countryLocationCommandRepository = countryLocationCommandRepository;
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Required Id").WithErrorCode("1")
            .Must(ChechIsExsitCountryLocation).WithMessage("CountryLocation With This Id Is Not Exist");

    }
    private bool ChechIsExsitCountryLocation(long countryLocationId)
    {
        return !_countryLocationCommandRepository.Exists(c => c.Id == countryLocationId);
    }
}

