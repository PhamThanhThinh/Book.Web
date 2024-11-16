using Book.Shared.Interfaces;
using Book.Web.Components;
using Book.Web.Data;
using Book.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
  var chuoiKetNoi = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseSqlServer(chuoiKetNoi);
  //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
  .AddAdditionalAssemblies(typeof(Book.Shared.Components.Pages.Books).Assembly);

//app.MapRazorComponents<App>();


app.Run();
