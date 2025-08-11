using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;
using LibrarySystem.Domains.Interfaces;
using LibrarySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services;

public class AuthorService : IAuthor
{
    #region Variables

    public readonly LibrarySystemDbContext _DbContext;

    #endregion

    #region Constructor

    public AuthorService(LibrarySystemDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a new author to the database.
    /// </summary>
    /// <param name="author">Author object</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> AddAsync(Author author)
    {
        try
        {
            if (author == null)
                return OpStatus.Failed;

            if (string.IsNullOrEmpty(author.FullName) || string.IsNullOrEmpty(author.Email))
            {
                return OpStatus.Failed;
            }

            if (await IsExist(author.Email))
            {
                return OpStatus.AlreadyExists;
            }
            else
            {
                author.CreatedAt = DateTime.Now;
                author.FullName = author.FullName.ToLower();
                author.Address = author.Address?.ToLower();
                author.Email = author.Email.ToLower();
                _DbContext.Authors.Add(author);
                await _DbContext.SaveChangesAsync();
                return OpStatus.successfully;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Deletes an author LOGICAL by setting their status to Deleted.
    /// </summary>
    /// <param name="id">Set id - Int value</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> DeleteAsync(int id)
    {
        try
        {
            if (await IsExistById(id))
            {
                var author = await GetAuthorByIdAsync(id);
                author.UserStatus = UserStatus.Deleted;
                _DbContext.Authors.Update(author);
                await _DbContext.SaveChangesAsync();
                return OpStatus.successfully;
            }
            else
            {
                return OpStatus.Failed;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// Gets all authors from the database.
    /// </summary>
    /// <returns>List of authors</returns>
    public async Task<List<Author>> GetAllAsync()
    {
        try
        {
            var authors = _DbContext.Authors.Where(a => a.Id > 0).ToListAsync();
            return await authors;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Gets an author by their Id.
    /// </summary>
    /// <param name="id">Set id - Int value</param>
    /// <returns>Author</returns>
    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        try
        {
            if (await IsExistById(id))
            {
                var author = await _DbContext.Authors.FindAsync(id);
                if (author != null)
                {
                    return author;
                }
            }

            return new Author();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Gets authors by their name.
    /// </summary>
    /// <param name="name">Set name - String value</param>
    /// <returns>List of authors</returns>
    public async Task<List<Author>> GetAuthorsByNameAsync(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return await GetAllAsync();
            }
            var authors = await _DbContext.Authors.Where(a => a.FullName.Contains(name.ToLower())).ToListAsync();
            return authors;
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// To update an existing author in the database.
    /// </summary>
    /// <param name="author">Author object</param>
    /// <returns>OpStatus</returns>
    public async Task<OpStatus> UpdateAsync(Author author)
    {
        try
        {
            var isExist = await _DbContext.Authors.AnyAsync(a => a.Id == author.Id);
            if (isExist)
            {
                var oldData = _DbContext.Authors.Find(author.Id);
                if (oldData == null)
                {
                    return OpStatus.Failed;
                }
                if (string.IsNullOrEmpty(author.FullName) || string.IsNullOrWhiteSpace(author.FullName)
                    || string.IsNullOrEmpty(author.Email) || string.IsNullOrWhiteSpace(author.Email)
                    || string.IsNullOrEmpty(author.Address) || string.IsNullOrWhiteSpace(author.Address)
                    )
                    return OpStatus.Failed;

                oldData.FullName = author.FullName?.ToLower();
                oldData.Email = author.Email?.ToLower();
                oldData.Address = author.Address?.ToLower();
                oldData.UserStatus = author.UserStatus;
                oldData.UpdatedAt = DateTime.Now; // Update DateTime is Now
                oldData.Id = oldData.Id; // Ensure the Id remains unchanged
                oldData.CreatedAt = oldData.CreatedAt; // Ensure the CreatedAt remains unchanged

                _DbContext.Authors.Update(oldData);
                await _DbContext.SaveChangesAsync();
                return OpStatus.successfully;
            }
            else
            {
                return OpStatus.Failed;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Checks if an author exists in the database by their email.
    /// </summary>
    /// <param name="email">Set email - String value</param>
    /// <returns>Return boolean value</returns>
    private async Task<bool> IsExist(string email)
    {
        try
        {
            var isEmailExist = _DbContext.Authors.AnyAsync(a => a.Email == email.ToLower());
            if (await isEmailExist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// checks if an author exists in the database by their Id.
    /// </summary>
    /// <param name="id">Author id - Int value</param>
    /// <returns>Return boolean value</returns>
    private async Task<bool> IsExistById(int id)
    {
        try
        {
            var isExist = await _DbContext.Authors.AnyAsync(a => a.Id == id);
            return isExist;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}
