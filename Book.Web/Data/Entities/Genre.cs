using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Web.Data.Entities
{
  public class Genre
  {
    // mã định danh
    [Key]
    public short Id { get; set; }

    [Required, MaxLength(50), Unicode(true)]
    public string Name { get; set; }
    //trường slug chứa đường liên kết của ứng dụng (hỗ trợ SEO website)
    // home/genre
    [Required, MaxLength(100), Unicode(true)]
    public string Slug { get; set; }

    // SEO, lên đầu trang, thể loại có thể được sắp xếp thủ công
    public bool IsTop { get; set; }

    public virtual ICollection<GenreBook> GenreBooks { get; set; } = new List<GenreBook>();
  }
}
