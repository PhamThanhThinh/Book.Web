using Book.Shared.DTOs;
using Book.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Book.Web.Services
{
  public class BookService
  {
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public BookService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
      _dbContextFactory = dbContextFactory;
    }

    // làm theo kiểu bất đồng bộ
    // task chức năng nhiệm vụ
    public async Task<GenreDTO[]> GetGenresAsync(bool isTopOnly)
    {
      using var context = _dbContextFactory.CreateDbContext();
      var query = context.Genres.AsQueryable();

      if (isTopOnly)
      {
        query = query.Where(g => g.IsTop);
      }

      //var genres = await query.ToListAsync();
      var genres = await query.Select(g => new GenreDTO(g.Name, g.Slug)).ToArrayAsync();

      return genres;
    }

    public async Task<PagedResult<BookListDTO>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null)
    {
      using var context = _dbContextFactory.CreateDbContext();

      var query = context.Books.AsQueryable();

      if (!string.IsNullOrWhiteSpace(genreSlug))
      {
        query = context.Genres
          .Where(g => g.Slug == genreSlug)
          .SelectMany(g => g.GenreBooks)
          .Select(gb => gb.Book);
      }

      var totalCount = await query.CountAsync();
      var books = await query.OrderByDescending(b => b.Id)
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .Select(b => new BookListDTO(b.Id, b.Name, b.Image, new AuthorDTO(b.Author.Name, b.Author.Slug)))
        .ToArrayAsync();

      return new PagedResult<BookListDTO>(books, totalCount);

    }

    public async Task<BookListDTO[]> GetPopularBookAsync(int count, string? genreSlug = null)
    {

    }

    public async Task<BookDetailsDTO> GetSimilarBookAsync(string bookId, int count)
    {

    }

    public async Task<PagedResult<BookListDTO>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
    {

    }
  }

}
