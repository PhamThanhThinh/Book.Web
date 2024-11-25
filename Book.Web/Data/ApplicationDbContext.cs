using Book.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book.Web.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<GenreBook> GenreBooks { get; set; }
    public DbSet<Books> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<GenreBook>()
        .HasKey(gb => new { gb.GenreId, gb.BookId });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    // tao database mac dinh
    private static void SeedData(ModelBuilder modelBuilder)
    {
      // kích hoạt khi add-migration và update - database
      modelBuilder.Entity<Genre>().HasData(
        [
          new Genre {
            Id = 1,
            Name = "Fantasy",
            Slug = "fantasy",
            IsTop = true,
          },
          new Genre {
            Id = 2,
            Name = "Action",
            Slug = "action",
            IsTop = true,
          },
          new Genre {
            Id = 3,
            Name = "Romance",
            Slug = "romance",
            IsTop = false,
          }
        ]
        );

      modelBuilder.Entity<Author>().HasData(
        [
          new Author {
            Id = 1,
            Name = "Robbie Morrison",
            Slug = "robbie-morrison",
          },
          new Author {
            Id = 2,
            Name = "George Mann",
            Slug = "george-mann",
          },
        ]
        );

      const string ImageBaseUrl = "https://m.media-amazon.com/images/I/81qr+D9GZNL._SY342_.jpg";

      modelBuilder.Entity<Books>().HasData(
        [
          new Books {
            Id = 1,
            Name = "Doctor Who: The Twelfth Doctor",
            Description = "The Twelfth Doctor has just regenerated – but the universe won’t give him a minute’s rest! Not when there’s a violent star on the warpath on a terraformed ice planet – or an ancient alien, masquerading as the celestial Okti, murdering her way towards resurrection in the year 2314!\r\n \r\nEnter the TARDIS with the Doctor and Clara for stunning new adventures! First, the pair battle an enemy who can slide between the cracks of the universe and take over unwilling human hosts – the FRACTURES! Then, the pair discovers an alien invasion in 1960s Las Vegas – forcing them to team up with gangsters!\r\n \r\nNext, The Doctor and Clara are back with a jaunt back to 1845, where the pair discover a horrifying secret hidden in a stately home! Then, the malevolent Hyperions return to scorch the solar system of all life – and the Doctor is pulled into in an epic war for the future of all humankind!",
            Format = "ebook",
            NumPages = 1000,
            Image = ImageBaseUrl,
            BuyLink = "https://www.amazon.com/Doctor-Who-Twelfth-Complete-Year/dp/1785864017/ref=sr_1_6?dib=eyJ2IjoiMSJ9.NoAVkqVqtlLYidHtWDzaQ2LV-8dWwURNVVVc3Dvikh_yteiN-jtLGf894x-OJFcoQNvKBCWnXMbg5ivPitmJkSjqEfXQfbtGhi-bLQOeUdIMwRbIA1qgUc-i6EUM-XD2028I2uL5-K05WAfFGRZBSPIHBbJ6-zrxhd9YcWop-uxYgsicqJfnISbf9Sv2TW7wS1eAB0cMt-FTfFj9nShLAIU2nuY6itI05uJrYOPzpnlE0lX1eX1-IyVNodptZN7VWvBqzyJmhyoJnms1YWyVRedtw_8_KYt_Jc4YE3QxJFo.DwY_HfsPX7Rn-DLGSADV5ew8mCEU0k8sXBUJiXaS2RE&dib_tag=se&keywords=Doctor+Who+Comic&qid=1730910659&sr=8-6",
            AuthorId = 1
          }
        ]
        );

      modelBuilder.Entity<GenreBook>().HasData(
        [
          new GenreBook {
            GenreId = 1,
            BookId = 1,
          }
        ]
        );
    }
  }
}
