namespace DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;

public class CountryLocationDto
{
    public long Id { get; set; }

    public byte LocationType { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public string AlternativeTitle { get; set; }

    public string Abbreviation { get; set; }

    public bool? IsPort { get; set; }

    public Guid? ParentCountryLocationId { get; set; }
    public Guid BusinessId { get; set; }
}
