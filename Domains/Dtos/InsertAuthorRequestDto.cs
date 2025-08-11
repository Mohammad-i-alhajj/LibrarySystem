using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domains.Dtos;

public class InsertAuthorRequestDto
{
    [MaxLength(60)]
    [Required]
    public string? FullName { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(60)]
    [Required]
    public string? Email { get; set; }
}
