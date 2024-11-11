using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book.Web.Data.Entities
{
  public class Books
  {
    // mã định danh
    [Key]
    public int Id { get; set; }

    // tên sách
    [Required, MaxLength(100), Unicode(true)]
    public string Name { get; set; }


    [Required, Unicode(true)]
    public string Description { get; set; }

    // phân loại: sách điện tử ebook, sách giấy
    [Required, MaxLength(100), Unicode(true)]
    public string Format { get; set; }

    // số trang của cuốn sách
    [Range(1, int.MaxValue)]
    public int NumPages { get; set; }


    // ảnh bìa
    [Required, MaxLength(200), Unicode(false)]
    public string Image { get; set; }

    // liên kết bán ở trang web khác
    //[MaxLength(4000)]
    public string? BuyLink { get; set; }

    public int AuthorId { get; set; }
    //[ForeignKey("AuthorId")]
    [ForeignKey(nameof(AuthorId))]
    public virtual Author Author { get; set; }

    //public virtual ICollection<GenreBook> GenreBooks { get; set; } = new List<GenreBook>();
    public virtual ICollection<GenreBook> BookGeres { get; set; } = new List<GenreBook>();
  }
}
