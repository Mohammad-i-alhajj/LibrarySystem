using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;

namespace LibrarySystem.Domains.Interfaces;

public interface IAuthor
{
    Task<OpStatus> AddAsync (Author author);
    Task<OpStatus> UpdateAsync(Author author);
    Task<OpStatus> DeleteAsync(int id);
    Task<List<Author>> GetAllAsync();
    Task<Author> GetAuthorByIdAsync(int id);
    Task<List<Author>> GetAuthorsByNameAsync(string name);
}
