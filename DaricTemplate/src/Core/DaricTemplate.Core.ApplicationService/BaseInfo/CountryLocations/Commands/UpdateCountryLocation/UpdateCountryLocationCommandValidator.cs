using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.UpdateCountryLocation;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using FluentValidation;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.UpdateCountryLocation;


public class UpdateCountryLocationCommandValidator : AbstractValidator<UpdateCountryLocationCommand>
{
    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;
    public UpdateCountryLocationCommandValidator(ICountryLocationCommandRepository countryLocationCommandRepository)
    {
        _countryLocationCommandRepository = countryLocationCommandRepository;

        RuleFor(c => c.Code)
            .NotNull().WithMessage("Required Code")
            .MaximumLength(10).WithMessage("Maximum Length Code 10 Char");
        RuleFor(c => c.Title)
            .NotNull().WithMessage("Required Title")
            .MinimumLength(5).WithMessage("Minimum Length Title 5 Char")
            .MaximumLength(255).WithMessage("Maximum Length Title 255 Char");
        RuleFor(c => c.AlternativeTitle)
            .MinimumLength(5).WithMessage("Minimum Length AlternativeTitle 5 Char")
            .MaximumLength(255).WithMessage("Maximum Length AlternativeTitle 255 Char");
        RuleFor(c => c.Abbreviation)
            .MinimumLength(5).WithMessage("Minimum Length Abbreviation 5 Char")
            .MaximumLength(30).WithMessage("Maximum Length Abbreviation 30 Char");
        RuleFor(c => c)
            .Must(ChechIsParentCountryLocationId).WithMessage("ParentCountryLocationId Is Required")
            .Must(ChechIsExsitCountryLocationCode).WithMessage("Code Is Duplicated")
            .Must(ChechIsExsitCountryLocationTitle).WithMessage("Title Is Duplicated");
        RuleFor(c => c.Id)
          .Must(ChechIsExsitCountryLocation).WithMessage("CountryLocation With This Id Is Not Exist");

    }
    private bool ChechIsExsitCountryLocationCode(UpdateCountryLocationCommand input)
    {
        return _countryLocationCommandRepository.Exists(c => input.ParentCountryLocationId.HasValue ? c.Code.Equals(input.Code) && c.ParentCountryLocationId.Equals(input.ParentCountryLocationId) : c.Code.Equals(input.Code));
    }
    private bool ChechIsExsitCountryLocationTitle(UpdateCountryLocationCommand input)
    {
        return _countryLocationCommandRepository.Exists(c => input.ParentCountryLocationId.HasValue ? c.Title.Equals(input.Title) && c.ParentCountryLocationId.Equals(input.ParentCountryLocationId) : c.Title.Equals(input.Title));
    }
    private bool ChechIsParentCountryLocationId(UpdateCountryLocationCommand input)
    {
        return input.LocationType != LocationType.Country && input.ParentCountryLocationId.HasValue;
    }
    private bool ChechIsExsitCountryLocation(long countryLocationId)
    {
        return !_countryLocationCommandRepository.Exists(c => c.Id == countryLocationId);
    }
}
