
using Daric.Core.Domain.Events;

namespace DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Events;
public class CountryLocationCreated : IDomainEvent
{
    public Guid BusinessId { get; private set; }
    public long Id { get; private set; }
    public byte LocationType { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string AlternativeTitle { get; set; }
    public string Abbreviation { get; set; } 
    public Guid? ParentCountryLocationId { get; set; }
    public CountryLocationCreated(Guid businessId,
        long id, 
        byte locationType,
        string code, 
        string title, 
        string alternativeTitle,
        string abbreviation, 
        Guid? parentCountryLocationId)
    {
        BusinessId = businessId;
        Id = id;
        LocationType = locationType;
        Code = code;
        Title = title;
        AlternativeTitle = alternativeTitle;
        Abbreviation = abbreviation; 
        ParentCountryLocationId = parentCountryLocationId;

    }
}
