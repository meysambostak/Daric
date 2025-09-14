using FluentValidation;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.CreateCountryLocation;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands;

namespace MiniTest.Core.ApplicationService.Commands.BaseInfo.CountryLocations.CreateCountryLocation;
public class CreateCountryLocationCommandValidator : AbstractValidator<CreateCountryLocationCommand>
{
    private readonly ICountryLocationCommandRepository _countryLocationCommandRepository;
    public CreateCountryLocationCommandValidator(ICountryLocationCommandRepository countryLocationCommandRepository)
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

    }
    private bool ChechIsExsitCountryLocationCode(CreateCountryLocationCommand input)
    {
        return _countryLocationCommandRepository.Exists(c => 
        input.ParentCountryLocationId.HasValue ?
                
                    c.Code.Equals(input.Code) && c.ParentCountryLocationId.Equals(input.ParentCountryLocationId)
                
                :
                   c.Code.Equals(input.Code)
                );
    }
    private bool ChechIsExsitCountryLocationTitle(CreateCountryLocationCommand input)
    {
        return _countryLocationCommandRepository.Exists(c => input.ParentCountryLocationId.HasValue ? c.Title.Equals(input.Title) && c.ParentCountryLocationId.Equals(input.ParentCountryLocationId) : c.Title.Equals(input.Title));
    }
    private bool ChechIsParentCountryLocationId(CreateCountryLocationCommand input)
    {
        return !input.LocationType.Equals(LocationType.Country) && input.ParentCountryLocationId.HasValue;
    }
}
