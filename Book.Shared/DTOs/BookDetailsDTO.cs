using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.DTOs
{
  // chi tiết thông tin sách
  public record BookDetailsDTO(int Id,
    string Name, string Image,
    AuthorDTO Author, int NumPages,
    string Description, GenreDTO[] Genres);
}
