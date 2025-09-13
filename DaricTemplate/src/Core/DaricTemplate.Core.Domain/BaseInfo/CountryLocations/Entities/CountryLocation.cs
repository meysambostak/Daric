
using Daric.Core.Domain.Entities;
using Daric.Core.Domain.ValueObjects.Common;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Events;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.ValueObjects;

namespace DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;

/// <summary>
/// جدول لوکیشن
/// </summary>
public class CountryLocation : AggregateRoot, IAuditableEntity
{
    #region Constructors
    private CountryLocation()
    {

    }
    private CountryLocation(byte locationType, Code code, Title title, string alternativeTitle, Abbreviation abbreviation, BusinessId parentCountryLocationId)
    {
        LocationType = locationType;
        Code = code;
        Title = title;
        AlternativeTitle = alternativeTitle;
        Abbreviation = abbreviation; 
        ParentCountryLocationId = parentCountryLocationId;
        AddEvent(new CountryLocationCreated(BusinessId.Value, Id, locationType, code.Value, title.Value, alternativeTitle, abbreviation.Value, parentCountryLocationId.Value));
    }
    #endregion

    #region Commands
    public static CountryLocation Create(byte locationType, Code code, Title title, string alternativeTitle, Abbreviation abbreviation,  BusinessId parentCountryLocationId)
        => new CountryLocation(locationType, code, title, alternativeTitle, abbreviation, parentCountryLocationId);
    #endregion

    public byte LocationType { get; set; }
    public Code Code { get; set; }
    public Title Title { get; set; }
    public string AlternativeTitle { get; set; }
    public Abbreviation Abbreviation { get; set; } 
    public BusinessId ParentCountryLocationId { get; set; }
    public virtual CountryLocation ParentCountryLocation { get; set; }
    public virtual ICollection<CountryLocation> CountryLocations { get; set; }
   
}


