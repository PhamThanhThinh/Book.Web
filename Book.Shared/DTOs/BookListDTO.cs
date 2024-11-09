using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.DTOs
{
  // danh sách sản phẩm cho trang home
  public record BookListDTO(int Id, string Name,
    string Image, AuthorDTO Author);
}
