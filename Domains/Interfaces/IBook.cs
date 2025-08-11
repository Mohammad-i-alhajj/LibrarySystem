using LibrarySystem.Domains.Dtos;
using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;

namespace LibrarySystem.Domains.Interfaces;

public interface IBook
{
    Task<OpStatus> AddAsync(InsertBookRequestDto book);
    Task<OpStatus> UpdateAsync(Book book);
    Task<OpStatus> DeleteAsync(int id);
    Task<List<Book>> GetAllAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<List<Book>> GetBooksByTitleAsync(string title);
}
