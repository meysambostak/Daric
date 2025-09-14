namespace DaricTemplate.Infra.Data.Queries.BaseInfo.CountryLocations;

public  class CountryLocation  
{
     public long Id { get; set; }
    public byte LocationType { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string AlternativeTitle { get; set; }
    public string Abbreviation { get; set; }
    public Guid ParentCountryLocationId { get; set; }  

}
