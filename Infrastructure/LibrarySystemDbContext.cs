using LibrarySystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure;

public class LibrarySystemDbContext : DbContext
{
    public LibrarySystemDbContext(DbContextOptions<LibrarySystemDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
}
