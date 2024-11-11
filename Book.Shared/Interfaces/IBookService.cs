using Book.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.Interfaces
{
  public interface IBookService
  {
    Task<BookDetailsDTO> GetBooksAsync(int bookId);
    Task<PagedResult<BookListDTO>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null);
    Task<PagedResult<BookListDTO>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug);
    Task<GenreDTO[]> GetGenresAsync(bool isTopOnly);
    Task<BookListDTO[]> GetPopularBookAsync(int count, string? genreSlug = null);
    Task<BookListDTO[]> GetSimilarBookAsync(int bookId, int count);
  }
}
