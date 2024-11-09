using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.DTOs
{
  public record struct GenreDTO(string Name, 
    string Slug);
}
