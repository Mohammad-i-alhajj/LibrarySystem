using LibrarySystem.Domains.Dtos;
using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;
using LibrarySystem.Domains.Interfaces;
using LibrarySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services;

public class BookService : IBook
{
    #region Variables

    public readonly LibrarySystemDbContext _DbContext;

    #endregion

    #region Constructor

    public BookService(LibrarySystemDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Insert a new book into the database
    /// </summary>
    /// <param name="book">InsertBookRequest object</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> AddAsync(InsertBookRequestDto request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrWhiteSpace(request.Title)
                || string.IsNullOrEmpty(request.Description) || string.IsNullOrWhiteSpace(request.Description)
                || string.IsNullOrEmpty(request.ISBN) || string.IsNullOrWhiteSpace(request.ISBN)
                || string.IsNullOrEmpty(request.Language) || string.IsNullOrWhiteSpace(request.Language)
                || string.IsNullOrEmpty(request.Category) || string.IsNullOrWhiteSpace(request.Category)
                || request.AuthorId <= 0)
            {
                return OpStatus.Failed;
            }

            var authorName = GetAuthorInformation(request.AuthorId);
            if (string.IsNullOrEmpty(authorName) || string.IsNullOrWhiteSpace(authorName))
            {
                return OpStatus.Failed;
            }

            var book = new Book();
            book.AuthorId = request.AuthorId;
            book.AuthorName = authorName;
            book.Category = request.Category?.ToLower();
            book.CreatedAt = DateTime.Now;
            book.Description = request.Description.ToLower();
            book.IsAvailable = IsAvailable.Available;
            book.ISBN = request.ISBN.ToLower();
            book.Language = request.Language.ToLower();
            book.NumberOfPages = request.NumberOfPages;
            book.Title = request.Title.ToLower();

            await _DbContext.Books.AddAsync(book);
            await _DbContext.SaveChangesAsync();
            return OpStatus.successfully;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// retrieve all books from the database
    /// </summary>
    /// <returns>List of books</returns>
    public async Task<List<Book>> GetAllAsync()
    {
        try
        {
            var books = await _DbContext.Books.Where(b => b.Id > 0).ToListAsync();
            if (books == null || books.Count == 0)
            {
                return new List<Book>();
            }
            return books;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// retrieve a book by its id
    /// </summary>
    /// <param name="id">Book id - Int value</param>
    /// <returns>Book</returns>
    public async Task<Book> GetBookByIdAsync(int id)
    {
        try
        {
            var isAlreadyExist = await IsExist(id);
            if (isAlreadyExist)
            {
                var book = await _DbContext.Books.FindAsync(id);
                return book;
            }
            return new Book();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// retrieve books by title
    /// </summary>
    /// <param name="title">Book title - String value</param>
    /// <returns>List of books</returns>
    public async Task<List<Book>> GetBooksByTitleAsync(string title)
    {
        try
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                return await GetAllAsync();
            }
            var books = await _DbContext.Books.Where(b => b.Title.Contains(title.ToLower())).ToListAsync();
            return books;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Update an existing book in the database
    /// </summary>
    /// <param name="book">Book object</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> UpdateAsync(Book book)
    {
        try
        {
            var isExist = await IsExist(book.Id);
            if (isExist)
            {
                var oldData = await GetBookByIdAsync(book.Id);

                if (oldData != null)
                {
                    if (string.IsNullOrEmpty(book.Title) || string.IsNullOrWhiteSpace(book.Title)
                        || string.IsNullOrEmpty(book.ISBN) || string.IsNullOrWhiteSpace(book.ISBN)
                        || string.IsNullOrEmpty(book.Description) || string.IsNullOrWhiteSpace(book.Description)
                        || string.IsNullOrEmpty(book.Language) || string.IsNullOrWhiteSpace(book.Language)
                        || string.IsNullOrEmpty(book.Category) || string.IsNullOrWhiteSpace(book.Category)
                        )
                        return OpStatus.Failed;

                    oldData.Id = oldData.Id;
                    oldData.CreatedAt = oldData.CreatedAt;
                    oldData.AuthorName = oldData.AuthorName;

                    oldData.Title = book.Title?.ToLower();
                    oldData.Description = book.Description?.ToLower();
                    oldData.ISBN = book.ISBN?.ToLower();
                    oldData.Language = book.Language?.ToLower();
                    oldData.NumberOfPages = book.NumberOfPages;
                    oldData.IsAvailable = book.IsAvailable;
                    oldData.Category = book.Category;

                    oldData.UpdatedAt = DateTime.Now;

                    _DbContext.Books.Update(oldData);
                    await _DbContext.SaveChangesAsync();
                    return OpStatus.successfully;
                }
            }

            return OpStatus.NotFound;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Delete a book by its id
    /// </summary>
    /// <param name="id">Book id - Int value</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> DeleteAsync(int id)
    {
        try
        {
            var isExist = await IsExist(id);
            if (isExist)
            {
                var book = await GetBookByIdAsync(id);
                if (book != null)
                {
                    book.IsAvailable = IsAvailable.NotAvailable;
                    _DbContext.Books.Update(book);
                    await _DbContext.SaveChangesAsync();
                    return OpStatus.successfully;
                }
                return OpStatus.Failed;
            }
            return OpStatus.NotFound;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    #endregion

    #region Private methods

    /// <summary>
    /// To get the author name by him id
    /// </summary>
    /// <param name="id">Set author id - Int value</param>
    /// <returns>Author name - string value</returns>
    private string? GetAuthorInformation(int id)
    {
        try
        {
            var author = _DbContext.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (author == null)
            {
                return string.Empty;
            }
            var authorName = author.Result?.FullName;
            return authorName;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Check if the book exists by id
    /// </summary>
    /// <param name="id">Book id - Int value</param>
    /// <returns>Boolean</returns>
    private async Task<bool> IsExist(int id)
    {
        try
        {
            var book = await _DbContext.Books.AnyAsync(b => b.Id == id);
            if (book)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    #endregion
}