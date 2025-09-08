using System;
using System.Linq;

namespace Daric.Core.Infrastructure.Extention;

public static class MappingExtension
{
    #region --- Methods ---

    public static string GetSchema(this Type type)
    {
        type?.Namespace?.CheckArgumentIsNull(nameof(type.Namespace));
        return type?.Namespace?.Split('.').Last();
    }

    public static string GetEntityNameWithSchema(this Type type)
    {
        return type.GetSchema() + "." + type.Name;
    }

    #endregion
}
