using System;
using System.Linq;
using Daric.Core.Infrastructure.Helpers.Guards;
using Daric.Core.Infrastructure.Helpers.Guards.GuardClauses;

namespace Daric.Core.Infrastructure.Helpers.Extensions;

public static class MappingExtension
{
    #region --- Methods ---

    public static string GetSchema(this Type type)
    {
        Guard.ThrowIf.NullArgument(type?.Namespace, nameof(type.Namespace));

        ///type?.Namespace?.CheckArgumentIsNull(nameof(type.Namespace));
        return type?.Namespace?.Split('.').Last();
    }

    public static string GetEntityNameWithSchema(this Type type)
    {
        return type.GetSchema() + "." + type.Name;
    }

    #endregion
}
