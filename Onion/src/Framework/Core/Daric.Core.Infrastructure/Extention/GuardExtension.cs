namespace Daric.Core.Infrastructure.Extention;

public static class GuardExtension
{

    public static void CheckArgumentIsNull(this object o, string name)
    {
        if (o == null)
        {
            throw new ArgumentNullException(name);
        }
    }

}
