using System.ComponentModel.DataAnnotations;

namespace RecipeCookingBook.API.Models;

public class Leftover
{
    public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    [Required]  public int Amount { get; set; }
}