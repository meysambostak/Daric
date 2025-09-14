using Daric.Core.Domain.ValueObjects.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
 

namespace DaricTemplate.Infra.Data.Commands.Common;

public class BusinessIdConversion :ValueConverter<BusinessId , Guid>
{
    public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
    {

    }
}
