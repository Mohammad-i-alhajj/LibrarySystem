using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domains.Dtos;

public class InsertBookRequestDto
{
    [MaxLength(100)]
    [Required]
    public string? Title { get; set; }

    [MaxLength(1000)]
    [Required]
    public string? Description { get; set; }

    [MaxLength(30)]
    [Required]
    public string? ISBN { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int NumberOfPages { get; set; }

    [MaxLength(2)]
    [Required]
    public string? Language { get; set; }

    [MaxLength(20)]
    [Required]
    public string? Category { get; set; }
}
