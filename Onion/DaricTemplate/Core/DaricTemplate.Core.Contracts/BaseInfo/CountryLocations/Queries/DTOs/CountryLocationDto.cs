namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;

public record CountryLocationDto

    (
    long Id,
    byte LocationType,
    string Code,
    string Title,
    string AlternativeTitle,
    string Abbreviation
    );
 
