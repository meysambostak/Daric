using Daric.Core.Domain.ValueObjects.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
 

namespace Daric.Infra.Data.Commands.ValueConversions;

public class BusinessIdConversion : ValueConverter<BusinessId, Guid>
{
    public BusinessIdConversion() : base(c => c.Value, c => BusinessId.FromGuid(c))
    {

    }
}
