using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Daric.Core.Domain.Enum;

public abstract class Enumeration //: IComparable
{
    public string DisplayName { get; private set; }

    public byte Value { get; private set; }

    protected Enumeration(byte value, string displayName) => (Value, DisplayName) = (value, displayName);

    public override string ToString() => DisplayName;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<T>();

    public override bool Equals(object obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    // public override byte GetHashCode() => (byte)Id.GetHashCode();


    public static T FromValue<T>(byte value) where T : Enumeration
    {
        T matchingItem = Parse<T, byte>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        T matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        T matchingItem = GetAll<T>().FirstOrDefault(predicate) ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

    //public byte CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);


    //public static bool operator !=(Enumeration right, Enumeration left) => !(right.Id == left.Id);
    //public static bool operator ==(Enumeration right, Enumeration left) => (right.Id == left.Id);
}
