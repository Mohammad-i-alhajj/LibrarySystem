using LibrarySystem.Domains.Entities;

namespace LibrarySystem.Domains.Dtos;

public class UpdateAuthorResponseDto
{
    public ResponseDto? AuthorResponse { get; set; }
    public Author? Author { get; set; }
}
