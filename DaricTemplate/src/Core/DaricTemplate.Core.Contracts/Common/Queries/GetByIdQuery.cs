using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daric.Core.Infrastructure.Queries;

namespace DaricTemplate.Core.Contracts.Common.Queries;

public class GetByIdQuery<T> : IQuery<T>
{
    public long Id { get; set; }
}

