using Book.Shared.DTOs;
using Book.Shared.Interfaces;
using Book.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Book.Web.Services
{
  public class BookService : IBookService
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
      using var context = _dbContextFactory.CreateDbContext();

      var query = context.Books.AsQueryable();

      if (!string.IsNullOrWhiteSpace(genreSlug))
      {
        query = context.Genres
          .Where(g => g.Slug == genreSlug)
          .SelectMany(g => g.GenreBooks)
          .Select(gb => gb.Book);
      }

      var books = await query
        .Select(b => new BookListDTO(b.Id, b.Name, b.Image, new AuthorDTO(b.Author.Name, b.Author.Slug)))
        .OrderBy(b => Guid.NewGuid())
        .Take(count)
        .ToArrayAsync();

      if (books.Length < count)
      {
        var bookIds = books.Select(b => b.Id);
        var addBooks = await context.Books
          .Where(b => !bookIds.Contains(b.Id))
          .Select(b => new BookListDTO(b.Id, b.Name, b.Image, new AuthorDTO(b.Author.Name, b.Author.Slug)))
          .OrderBy(b => Guid.NewGuid())
          .Take(count - books.Length)
          .ToArrayAsync();

        // gộp books và addBooks (cú pháp cũ)
        //books = [.. books, .. addBooks];

        // gộp books và addBooks (cú pháp mới)
        books = books.Concat(addBooks).ToArray();

      }


      return books;
    }

    public async Task<BookDetailsDTO> GetBooksAsync(int bookId)
    {
      using var context = _dbContextFactory.CreateDbContext();

      //var book = await context.Books.Where
      //  (b => b.Id == bookId)
      //  .Select(b => new BookDetailsDTO
      //  (b.Id, b.Name, b.Image,
      //  new AuthorDTO
      //  (b.Author.Name, b.Author.Slug), b.NumPages,
      //  b.Format, b.Description, b.BookGeres
      //  .Select(bg => new GenreDTO
      //  (bg.Genre.Name, bg.Genre.Slug)).ToArray()
      //  ))
      //  .FirstOrDefaultAsync();

      var book = await context.Books
      .Where(b => b.Id == bookId)
      .Include(b => b.Author)  // Nạp thông tin tác giả
      .Include(b => b.BookGeres) // Nạp danh sách thể loại của sách
          .ThenInclude(bg => bg.Genre) // Nạp thông tin thể loại
      .Select(b => new BookDetailsDTO(
          b.Id,
          b.Name,
          b.Image,
          new AuthorDTO(
              b.Author.Name,
              b.Author.Slug
          ),
          b.NumPages,
          b.Format,
          b.Description,
          b.BookGeres.Select(bg => new GenreDTO(
              bg.Genre.Name,
              bg.Genre.Slug
          )).ToArray(),
          b.BuyLink
      ))
      .FirstOrDefaultAsync();

      return book;
    }

    public async Task<BookListDTO[]> GetSimilarBookAsync(int bookId, int count)
    {
      using var context = _dbContextFactory.CreateDbContext();

      //var similarBooks = await context.GenreBooks.Where(gb => gb.BookId == bookId).Sel

      //var similarBooks = await context.GenreBooks
      //  .Where(gb => gb.BookId == bookId)
      //  .SelectMany(gb => context.GenreBooks
      //      .Where(similar => similar.GenreId == gb.GenreId && similar.BookId != bookId)
      //      .Select(similar => similar.Book))
      //  .ToListAsync();

      var similarBooks = await context.GenreBooks
        .Where(gb => gb.BookId == bookId)
        .SelectMany(gb => gb.Genre.GenreBooks)
        .Select(gb => gb.Book)
        .Where(b => b.Id != bookId)
        .Select(b => new BookListDTO(b.Id, b.Name, b.Image, new AuthorDTO(b.Author.Name, b.Author.Slug)))
        .OrderBy(b => Guid.NewGuid())
        .Take(count)
        .ToArrayAsync();

      return similarBooks;

    }

    public async Task<PagedResult<BookListDTO>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
    {
      using var context = _dbContextFactory.CreateDbContext();

      var query = context.Books.Where(b => b.Author.Slug == authorSlug);

      var totalCount = await query.CountAsync();

      var books = await query
        .Select(b => new BookListDTO(b.Id, b.Name, b.Image, new AuthorDTO(b.Author.Name, b.Author.Slug)))
        .OrderByDescending(b => b.Id)
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .ToArrayAsync();

      return new PagedResult<BookListDTO>(books, totalCount);
    }
  }
}
