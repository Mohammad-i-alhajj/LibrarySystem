using LibrarySystem.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domains.Entities;

public class Author : BaseClass
{
    [MaxLength(60)]
    [Required]
    public string? FullName { get; set; }
    
    [MaxLength(500)]
    public string? Address { get; set; }
    
    [MaxLength(60)]
    [Required]
    public string? Email { get; set; }

    public UserStatus UserStatus { get; set; } = UserStatus.Active;
}
