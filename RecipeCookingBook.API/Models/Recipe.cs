using System.ComponentModel.DataAnnotations;

namespace RecipeCookingBook.API.Models;

public class Recipe
{
    public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    public int PreparationTimeInMinutes { get; set; }

    public int Portions { get; set; }

    [Required] public string Category { get; set; } = string.Empty;

    [Required] public string Instructions { get; set; } = string.Empty;

    [Required]   public IList<Ingredient>? Ingredients { get; set; }
}