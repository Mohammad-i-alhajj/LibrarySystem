using LibrarySystem.Domains.Enums;

namespace LibrarySystem.Domains.Dtos;

public class ResponseDto
{
    public string? Message { set; get; }
    public OpStatus OpStatus { set; get; }
}
