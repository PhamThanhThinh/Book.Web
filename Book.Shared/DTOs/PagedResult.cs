using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.DTOs
{
  public record PagedResult<TRecord>(TRecord[] Records, int totalCount);

}
