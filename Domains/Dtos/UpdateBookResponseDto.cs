using LibrarySystem.Domains.Entities;

namespace LibrarySystem.Domains.Dtos;

public class UpdateBookResponseDto
{
    public ResponseDto? BookResponse { get; set; }
    public Book? Book { get; set; }
}
