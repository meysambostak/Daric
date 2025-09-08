

using Daric.Core.Domain.ValueObjects;

namespace DaricTemplate.Core.Domain.BaseInfo.CountryLocations.ValueObjects;

public class Code : BaseValueObject<Code>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Code FromString(string value) => new(value);
    public Code(string value)
    {
        //todo


        //if (!string.IsNullOrWhiteSpace(value) && value.Length > 500)
        //{
        //    throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(Username), "0", "500");
        //}

        Value = value;
    }
    private Code()
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
    public static explicit operator string(Code code) => code.Value;

    public static implicit operator Code(string value) => new(value);
    #endregion

}

