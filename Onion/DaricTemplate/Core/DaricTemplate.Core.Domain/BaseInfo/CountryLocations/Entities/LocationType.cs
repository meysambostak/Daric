

using Daric.Core.Domain.Enum;

namespace DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;

public class LocationType
    : Enumeration
{
    public readonly static LocationType Country = new(1, nameof(Country));
    public readonly static LocationType Province = new(2, nameof(Province));
    public readonly static LocationType City = new(3, nameof(City));

    public LocationType(byte value, string displayName)
        : base(value, displayName)
    {
    }
}

