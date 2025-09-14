
using Daric.Core.Domain.ValueObjects;

namespace DaricTemplate.Core.Domain.BaseInfo.CountryLocations.ValueObjects;
public class Abbreviation : BaseValueObject<Abbreviation>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Abbreviation FromString(string value) => new(value);
    public Abbreviation(string value)
    {
        //todo


        //if (!string.IsNullOrWhiteSpace(value) && value.Length > 500)
        //{
        //    throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(Username), "0", "500");
        //}

        Value = value;
    }
    private Abbreviation()
    {
    }
    #endregion

    #region Equality Check
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operator Overloading
    public static explicit operator string(Abbreviation abbreviation) => abbreviation.Value;

    public static implicit operator Abbreviation(string value) => new(value);
    #endregion

}
