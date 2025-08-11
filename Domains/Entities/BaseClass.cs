using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domains.Entities;

public class BaseClass
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
