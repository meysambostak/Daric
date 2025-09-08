using Daric.Core.Domain.ValueObjects.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
 

namespace DaricTemplate.Infra.Data.Commands.Common;

public class TitleConversion : ValueConverter<Title, string>
{
    public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
    {

    }
}
