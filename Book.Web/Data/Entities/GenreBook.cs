using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book.Web.Data.Entities
{
  public class GenreBook
  {
    [Key, Column(Order = 0)]
    public int BookId { get; set; }

    [Key, Column(Order = 1)]
    public short GenreId { get; set; }

    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }
    [ForeignKey(nameof(GenreId))]
    public virtual Genre Genre { get; set; }
  }
}
