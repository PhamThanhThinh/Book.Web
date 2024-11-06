using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Book.Web.Data.Entities
{
  public class Author
  {
    // mã định danh
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100), Unicode(true)]
    public string Name { get; set; }
    [Required, MaxLength(100), Unicode(true)]

    // ví dụ: home/author
    public string Slug { get; set; }
  }
}
