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
    AuthorDTO Author, int NumPages, string Format,
    string Description, GenreDTO[] Genres, string? BuyLink)
  {
    //public string BuyLink => string.IsNullOrWhiteSpace(BuyLink) ? string.Empty : BuyLink;
    //public string BuyLink => string.IsNullOrWhiteSpace(BuyLink) ? $"https://www.amazon.com/Doctor-Who-Empire-Wolf/dp/1787736431/ref=sr_1_1?dib=eyJ2IjoiMSJ9.4EoZD_T1OPJcULy2Bw9wXE-aGCP8TmT3daaEsNvE7jjyteiN-jtLGf894x-OJFcoQNvKBCWnXMbg5ivPitmJkYonVIT9izp-d5ZO81ZAm4jBtC2gDT6QWuZo_0CmWaQuliqYTU3Up9OCNwvyx3-m9_IHBbJ6-zrxhd9YcWop-uwppGfsih6FWKuune5U41ESGV26u_RLmjvA8hm49DP6usqzXfPNvfi_5X3ig7Jl6OKDrsoj3bVykuh1qD4f-kbvbFRBVyFfV3haH0izVgSx6AFd_QXZ8Atht_zeMumKuG4.U9ypkrShb2FizBILODsHC2-fhCTyyh57XiQ75mlMR5U&dib_tag=se&keywords=Doctor+Who+Comic&qid=1732022688&sr=8-1" : BuyLink;
    public string BookBuyLink => string.IsNullOrWhiteSpace(BuyLink) ? $"https://www.google.com/search?q={Name.Replace(" ", "+")} +by+ {Author.Name.Replace(" ", "+")}" : BuyLink;
    
    //public string BookBuyLink =>
    //string.IsNullOrWhiteSpace(BuyLink)
    //    ? $"https://www.google.com/search?q={Uri.EscapeDataString((Name ?? "").Replace(" ", "+"))}+by+{Uri.EscapeDataString((Author.Name ?? "").Replace(" ", "+"))}"
    //    : BuyLink;

  }
}
