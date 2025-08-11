using LibrarySystem.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domains.Entities;

public class Book : BaseClass
{
    [MaxLength(100)]
    [Required]
    public string? Title { get; set; }

    [MaxLength(1000)]
    [Required]
    public string? Description { get; set; }

    [MaxLength(60)]
    [Required]
    public string? AuthorName { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [MaxLength(30)]
    [Required]
    public string? ISBN { get; set; }

    [MaxLength(2)]
    [Required]
    public string? Language { get; set; }

    [Required]
    public int NumberOfPages { get; set; }

    [Required]
    public IsAvailable IsAvailable { get; set; }

    [MaxLength(20)]
    [Required]
    public string? Category { get; set; }
}
